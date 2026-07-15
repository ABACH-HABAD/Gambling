using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Sessions;
using Gambling.Application.Core.Api.Requests;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.Services.Token;
using Gambling.Core.Exceptions;
using Gambling.WebApi.Classes;
using Gambling.WebApi.Classes.Services;

namespace Gambling.WebApi.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/login", async (HttpContext httpContext, LoginRequest data, IAccountService accountService) =>
        {
            LoginResult result;
            try
            {
                result = await accountService.LoginAsync(data.Login, data.Password, deviceType: (DeviceType)data.DeviceType, ip: httpContext.GetIp(), loginAsAdmin: data.LoginAsAdmin ?? false);
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
            LoginResult result = await accountService.RegistrateAsync(data.Login, data.Password, data.RepeatPassword, (DeviceType)data.DeviceType, httpContext.GetIp());
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
            try
            {
                LoginResult loginResult = await loginService.CheckActiveLoginAsync(request.Token, request.DeviceType, httpContext.GetIp());
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
            RefreshedTokens? tokens = await sessionService.RefreshTokenAsync(request.Token, request.DeviceType, httpContext.GetIp());
            if (tokens != null)
            {
                return Results.Ok(tokens);
            }
            else return Results.Unauthorized();
        });

        app.MapPut("/changeEmail", async (ChangeEmailRequest request, HttpContext httpContext, IAccountService accountService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, request, out int userId, out IResult? result)) return result;
            try
            {
                return Results.Ok(await accountService.ChangeEmailAsync(userId, request.OldEmail, request.NewEmail));
            }
            catch
            {
                return Results.BadRequest();
            }
        }).RequireAuthorization();

        app.MapPut("/changePassword", async (ChangePasswordRequest request, HttpContext httpContext, IAccountService accountService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, request, out int userId, out IResult? result)) return result;

            try
            {
                return Results.Ok(await accountService.ChangePasswordAsync(userId, request.OldHashedPassword, request.NewHashedPassword, request.RepeatHashedPassword, request.ForceChange));
            }
            catch
            {
                return Results.BadRequest();
            }
        }).RequireAuthorization();

        app.MapPost("/logout", async (RefreshTokenRequest request, HttpContext httpContext, ISessionService sessionService) =>
        {
            try
            {
                await sessionService.Logout(request.Token, request.DeviceType, httpContext.GetIp());
                return Results.Ok();
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();
    }
}