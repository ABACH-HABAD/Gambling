using BusinessLogic.Account.Auth;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Exceptions;
using BusinessLogic.Token;
using DataBaseClasses.Exceptions;
using MySqlX.XDevAPI;
using System.Security.Claims;

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
                result = await accountService.LoginAsync(data.Login, data.Password, deviceType: (DeviceType)data.DeviceType, ip: clientIp, loginAsAdmin: data.LoginAsAdmin ?? false);
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

            LoginResult result = await accountService.RegistrateAsync(data.Login, data.Password, data.RepeatPassword, (DeviceType)data.DeviceType, clientIp);
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

        app.MapPut("/changeEmail", async (ChangeEmailRequest request, HttpContext httpContext, IAccountService accountService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"Получен запрос /logout с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                return Results.Ok(await accountService.ChangeEmailAsync(userId, request.OldEmail, request.NewEmail));
            }
            catch
            {
                return Results.BadRequest();
            }
        });

        app.MapPut("/changePassword", async (ChangePasswordRequest request, HttpContext httpContext, IAccountService accountService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"Получен запрос /logout с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                return Results.Ok(await accountService.ChangePasswordAsync(userId, request.OldHashedPassword, request.NewHashedPassword, request.RepeatHashedPassword));
            }
            catch
            {
                return Results.BadRequest();
            }
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
