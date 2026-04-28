using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DataBaseClasses;
using DataBaseClasses.Entity;
using DataBaseClasses.Repository;
using DataBaseClasses.Repository.Interfaces;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Auth;
using BusinessLogic.Auth.Validation;
using BusinessLogic.Game.Blackjack;
using BusinessLogic.Game.Roulette;
using BusinessLogic.Game.Slots;
using BusinessLogic.Profile;
using BusinessLogic.Token;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

string basePath = AppContext.BaseDirectory;
string appsettingsPath = Path.Combine(basePath, "appsettings.json");
builder.Configuration.AddJsonFile(appsettingsPath, optional: false, reloadOnChange: true);

IConfigurationSection jwtConfig = builder.Configuration.GetSection("JwtSettings");

JwtSettings jwtSettings = new(jwtConfig["SecretKey"] ?? throw new Exception("JWT ключ не настроен"),
                               jwtConfig["Issuer"] ?? throw new Exception("Issuer не настроен"),
                               jwtConfig["Audience"] ?? throw new Exception("Audience не настроен"),
                               5);

SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.FromMinutes(5),
            RequireSignedTokens = true,
            ValidateSignatureLast = false,
            ConfigurationManager = null
        };/*
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"JWT Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"JWT Challenge (401): {context.Error}, {context.ErrorDescription}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"JWT Token validated successfully");
                return Task.CompletedTask;
            }
        };*/
    });

string dataBaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new CannotFindConnectionString();

builder.Services.AddSingleton(new DataBaseConnectionString(dataBaseConnectionString));
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySQL(dataBaseConnectionString));

builder.Services.AddAuthorization();

builder.Services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddScoped<IGameTypesRepository, GameTypesRepository>();
builder.Services.AddScoped<IDeviceTypeRepository, DeviceTypesRepository>();
builder.Services.AddScoped<IUserStatusesRepository, UserStatusesRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddScoped<ISlotsWinCounterService, SlotsWinCounterService>();
builder.Services.AddScoped<ISlotsCombinationCounterService, SlotsCombinationCounterService>();
builder.Services.AddScoped<ISlotsService, ServerSlotsService>();

builder.Services.AddScoped<IBlackjackService, ServerBlackjackService>();

builder.Services.AddScoped<IRouletteService, ServerRouletterService>();
builder.Services.AddScoped<IRouletteWinCounterService, ServerRouletteWinCounterService>();

builder.Services.AddTransient<IValidation, EmailValidation>();
builder.Services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();
builder.Services.AddScoped<IAccountService, ServerAccountService>();
builder.Services.AddScoped<ISessionService, ServerSessionService>();
builder.Services.AddScoped<ILoginChecker, ServerSessionService>();

builder.Services.AddTransient<IUserStatisticsService, UserStatisticsService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();


builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login", async (HttpContext httpContext, LoginRequest data, IAccountService accountService) =>
{
    string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

    Console.WriteLine($"Получен запрос /login с IP: {clientIp}");

    LoginResult result = await accountService.LoginAsync(data.Login, data.Password, deviceType: (BusinessLogic.Auth.DeviceType)data.DeviceType, ip: clientIp);
    if (result.Result)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.Json(result, statusCode: StatusCodes.Status401Unauthorized, contentType: "application/json");
    }
});

app.MapPost("/registrate", async (HttpContext httpContext, RegistrateRequest data, IAccountService accountService) =>
{
    string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

    Console.WriteLine($"Получен запрос /registrate с IP: {clientIp}");

    LoginResult result = await accountService.RegistrateAsync(data.Login, data.Password, data.RepeatPassword, (BusinessLogic.Auth.DeviceType)data.DeviceType, clientIp);
    if (result.Result)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.Json(result, statusCode: StatusCodes.Status401Unauthorized, contentType: "application/json");
    }
});

app.MapPost("/autoLogin", async (RefreshTokenRequest request, HttpContext httpContext, ILoginChecker loginService) =>
{
    string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

    Console.WriteLine($"Получен запрос /autoLogin с IP: {clientIp}");

    if (clientIp == null) return Results.Unauthorized();

    try
    {
        LoginResult loginResult = await loginService.CheckActiveLoginAsync(request.Token, request.DeviceType, clientIp);
        if (loginResult.Result)
        {
            return Results.Ok(loginResult);
        }
        else return Results.Unauthorized();
    }
    catch (NoConnectionException)
    {
        return Results.Unauthorized();
    }
});

app.MapPost("/refresh", async (RefreshTokenRequest request, HttpContext httpContext, ISessionService sessionService) =>
{
    string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
    Console.WriteLine($"Получен запрос /refresh с IP: {clientIp}");

    var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim is null)
        return Results.Unauthorized();

    RefreshedTokens? tokens = await sessionService.RefreshTokenAsync(request.Token, request.DeviceType, clientIp);
    if (tokens != null)
    {
        return Results.Ok(tokens);
    }
    else return Results.Unauthorized();
});

app.MapPost("/logout", async (RefreshTokenRequest request, HttpContext httpContext, ISessionService sessionService) =>
{
    string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
    Console.WriteLine($"Получен запрос /logout с IP: {clientIp}");

    var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim is null)
        return Results.Unauthorized();

    int userId = int.Parse(userIdClaim.Value);

    await sessionService.Logout(request.Token, request.DeviceType, clientIp);

    return Results.Ok();

}).RequireAuthorization();

app.MapGet("/getUserData", async (HttpContext httpContext, IAccountService accountService) =>
{
    string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
    Console.WriteLine($"Получен запрос /getUserData с IP: {clientIp}");

    var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim is null)
        return Results.Unauthorized();

    int userId = int.Parse(userIdClaim.Value);

    User? user = await accountService.GetUserData(userId);
    if (user != null) return Results.Ok(user);
    else return Results.BadRequest();
}).RequireAuthorization();

app.MapPost("/spinSlots", async (HttpContext httpContext, SlotSpinRequest slotSpinRequest, ISlotsService slotService) =>
{
    string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

    Console.WriteLine($"Получен запрос /spinSlots с IP: {clientIp}");

    var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim is null)
        return Results.Json(new SlotGameResult(false, "Токен сессии недействителен или остутствует", 0, null!), statusCode: StatusCodes.Status400BadRequest, contentType: "application/json");

    int userId = int.Parse(userIdClaim.Value);

    try
    {
        SlotGameResult? slotGameResult = await slotService.Spin(userId, slotSpinRequest.Bid, slotSpinRequest.LinesCount, slotSpinRequest.ColumnsCount);
        if (slotGameResult != null) return Results.Ok(slotGameResult);
        else return Results.Json(slotGameResult, statusCode: StatusCodes.Status400BadRequest, contentType: "application/json");

    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

}).RequireAuthorization();

app.MapPost("/spinRoulette", async (HttpContext httpContext, RouletteSpinRequest rouletteSpinRequest, IRouletteService rouletteService) =>
{
    string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

    Console.WriteLine($"Получен запрос /spinRoulette с IP: {clientIp}");

    var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim is null)
        return Results.Json(new RouletteGameResult(false, "Токен сессии недействителен или остутствует", 0, 0), statusCode: StatusCodes.Status400BadRequest, contentType: "application/json");

    int userId = int.Parse(userIdClaim.Value);

    try
    {
        RouletteGameResult? rouletteGameResult = await rouletteService.Spin(userId, rouletteSpinRequest.Bids.PrepareToJson());
        if (rouletteGameResult != null)
            return Results.Ok(rouletteGameResult);
        else return Results.Json(rouletteGameResult, statusCode: StatusCodes.Status400BadRequest, contentType: "application/json");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

}).RequireAuthorization();


app.Run();