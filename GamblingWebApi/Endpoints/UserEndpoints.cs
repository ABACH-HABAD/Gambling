using System.Security.Claims;
using DataBaseClasses.Exceptions;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Auth;
using BusinessLogic.Exceptions;
using BusinessLogic.Token;

namespace GamblingWebApi.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/login", async (HttpContext httpContext, LoginRequest data, IAccountService accountService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /login с IP: {clientIp}");

            LoginResult result;
            try
            {
                result = await accountService.LoginAsync(data.Login, data.Password, deviceType: (BusinessLogic.Auth.DeviceType)data.DeviceType, ip: clientIp, loginAsAdmin: data.LoginAsAdmin ?? false);
            }
            catch (InsufficientRightsException)
            {
                return Results.Forbid();
            }

            if (result.Result)
            {
                return Results.Ok(result);
            }
            else
            {
                return Results.Json(result, statusCode: StatusCodes.Status401Unauthorized, contentType: "application/json");
            }
        });

        app.MapPost("/registrate", async (RegistrateRequest data, HttpContext httpContext, IAccountService accountService) =>
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
                else return Results.Json(loginResult, statusCode: StatusCodes.Status401Unauthorized, contentType: "application/json");
            }
            catch (NoConnectionException)
            {
                return Results.Json(new LoginResult(null, false, "Нет подключения к базе данных"), statusCode: StatusCodes.Status401Unauthorized, contentType: "application/json");
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
    }
}
