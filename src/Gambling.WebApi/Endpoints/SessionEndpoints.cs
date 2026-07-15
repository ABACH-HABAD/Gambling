using Gambling.Application.Core.Abstractions.Sessions;
using Gambling.WebApi.Classes;
using Gambling.WebApi.Classes.Services;

namespace Gambling.WebApi.Endpoints;

public static class SessionEndpoints
{
    public static void MapSessionsEndpoints(this WebApplication app)
    {
        app.MapGet("/getAllSessions", async (HttpContext httpContext, ISessionStorageService sessionStorageService, IIdCheckerService idChecker) =>
        {
            return Results.Ok(await sessionStorageService.GetAllSessionsAsync());

        }).RequireAuthorization(policy => policy.RequireRole(AdminRole.Id));
    }
}