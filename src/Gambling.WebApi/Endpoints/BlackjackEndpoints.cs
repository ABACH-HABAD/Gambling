using Gambling.Application.Core.Abstractions.Game.Blackjack;
using Gambling.Application.Core.Api.Requests;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;
using Gambling.WebApi.Classes.Services;

namespace Gambling.WebApi.Endpoints;

public static class BlackjackEndpoints
{
    public static void MapBlackjackEndpoints(this WebApplication app)
    {
        app.MapGet("/blackjack/tryContinueGame", async (HttpContext httpContext, IBlackjackService blackjackService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormContext(httpContext, out int userId, out IResult? result)) return result;

            try
            {
                BlackjackGameState blackjackGameState = await blackjackService.TryContinueGameAsync(userId);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();

        app.MapPost("/blackjack/firstMove", async (HttpContext httpContext, BlackjackRequest request, IBlackjackService blackjackService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormContext(httpContext, out int userId, out IResult? result)) return result;

            try
            {
                BlackjackGameState blackjackGameState = await blackjackService.FirstMoveAsync(userId, request.Bet);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();

        app.MapPost("/blackjack/takeCard", async (HttpContext httpContext, IBlackjackService blackjackService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormContext(httpContext, out int userId, out IResult? result)) return result;

            try
            {
                BlackjackGameState blackjackGameState = await blackjackService.TakeCardAsync(userId);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();

        app.MapPost("/blackjack/takeDouble", async (HttpContext httpContext, IBlackjackService blackjackService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormContext(httpContext, out int userId, out IResult? result)) return result;

            try
            {
                BlackjackGameState blackjackGameState = await blackjackService.TakeDoubleAsync(userId);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();

        app.MapPost("/blackjack/stand", async (HttpContext httpContext, IBlackjackService blackjackService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormContext(httpContext, out int userId, out IResult? result)) return result;

            try
            {
                BlackjackGameState blackjackGameState = await blackjackService.StandAsync(userId);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();

        app.MapPost("/blackjack/endGame", async (HttpContext httpContext, IBlackjackService blackjackService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormContext(httpContext, out int userId, out IResult? result)) return result;

            try
            {
                BlackjackGameResult blackjackGameState = await blackjackService.EndGameAsync(userId);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();
    }
}