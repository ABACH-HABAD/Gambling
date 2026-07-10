using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Statistics;
using Gambling.Application.Core.Api.Results;
using Gambling.WebApi.Classes;

namespace Gambling.WebApi.Endpoints;

public static class UserStatisticEndpoints
{
    public static void MapUserStatisticEndpoints(this WebApplication app)
    {
        app.MapGet("/userStatistic", async (HttpContext httpContext, int gameType, int userId, IUserStatisticsService userStatisticsService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;

            try
            {
                UserStatisticResult result = await userStatisticsService.GetUserStatisticResultAsync(userId, (GameType)gameType);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/frequency", async (HttpContext httpContext, int gameType, int userId, IUserStatisticsService userStatisticsService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;

            try
            {
                double result = await userStatisticsService.GetWinFrequencyAsync(userId, (GameType)gameType);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/count/win", async (HttpContext httpContext, int gameType, int userId, IUserStatisticsService userStatisticsService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;

            try
            {
                return Results.Ok(await userStatisticsService.GetWinCountAsync(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/count/loss", async (HttpContext httpContext, int gameType, int userId, IUserStatisticsService userStatisticsService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;

            try
            {
                return Results.Ok(await userStatisticsService.GetLossCountAsync(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/count/total", async (HttpContext httpContext, int gameType, int userId, IUserStatisticsService userStatisticsService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;

            try
            {
                return Results.Ok(await userStatisticsService.GetTotalCountAsync(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/balance/win", async (HttpContext httpContext, int gameType, int userId, IUserStatisticsService userStatisticsService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;

            try
            {
                return Results.Ok(await userStatisticsService.GetWinBalanceAsync(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/balance/loss", async (HttpContext httpContext, int gameType, int userId, IUserStatisticsService userStatisticsService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;

            try
            {
                return Results.Ok(await userStatisticsService.GetLossBalanceAsync(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();

        app.MapGet("/userStatistic/balance/total", async (HttpContext httpContext, int gameType, int userId, IUserStatisticsService userStatisticsService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;

            try
            {
                return Results.Ok(await userStatisticsService.GetTotalBalanceAsync(userId, (GameType)gameType));
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization();
    }
}