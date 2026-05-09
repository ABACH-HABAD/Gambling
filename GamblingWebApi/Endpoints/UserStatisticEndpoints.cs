using System.Security.Claims;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Game;
using BusinessLogic.Profile.Statistics;

namespace GamblingWebApi.Endpoints;

public static class UserStatisticEndpoints
{
    public static void MapUserStatisticEndpoints(this WebApplication app)
    {
        app.MapGet("/userStatistic/frequency", async (HttpContext httpContext, int gameType, IUserStatisticsService userStatisticsService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /userStatistic/frequency с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                double result = await userStatisticsService.WinFrequency(userId, (GameType)gameType);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/count/win", async (HttpContext httpContext, int gameType, IUserStatisticsService userStatisticsService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /userStatistic/count/win с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                return Results.Ok(await userStatisticsService.WinCount(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/count/loss", async (HttpContext httpContext, int gameType, IUserStatisticsService userStatisticsService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /userStatistic/count/loss с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                return Results.Ok(await userStatisticsService.LossCount(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/count/total", async (HttpContext httpContext, int gameType, IUserStatisticsService userStatisticsService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /userStatistic/count/total с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                return Results.Ok(await userStatisticsService.TotalCount(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/balance/win", async (HttpContext httpContext, int gameType, IUserStatisticsService userStatisticsService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /userStatistic/balance/win с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                return Results.Ok(await userStatisticsService.WinBalance(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/balance/loss", async (HttpContext httpContext, int gameType, IUserStatisticsService userStatisticsService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /userStatistic/balance/loss с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                return Results.Ok(await userStatisticsService.LossBalance(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/balance/total", async (HttpContext httpContext, int gameType, IUserStatisticsService userStatisticsService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /userStatistic/balance/total с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                return Results.Ok(await userStatisticsService.TotalBalance(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();
    }
}
