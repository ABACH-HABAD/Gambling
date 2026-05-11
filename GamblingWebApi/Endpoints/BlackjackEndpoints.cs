using System.Security.Claims;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Game.Blackjack;

namespace GamblingWebApi.Endpoints;

public static class BlackjackEndpoints
{
    public static void MapBlackjackEndpoints(this WebApplication app)
    {
        app.MapPost("/blackjack/firstMove", async (HttpContext httpContext, BlackjackRequest request, IBlackjackService blackjackService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /blackjack/firstMove с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                BlackjackGameState blackjackGameState = await blackjackService.FirstMove(userId, request.Bet);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();

        app.MapPost("/blackjack/takeCard", async (HttpContext httpContext, IBlackjackService blackjackService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /blackjack/takeCard с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                BlackjackGameState blackjackGameState = await blackjackService.TakeCard(userId);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();

        app.MapPost("/blackjack/takeDouble", async (HttpContext httpContext, IBlackjackService blackjackService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /blackjack/takeDouble с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                BlackjackGameState blackjackGameState = await blackjackService.TakeDouble(userId);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();

        app.MapPost("/blackjack/stand", async (HttpContext httpContext, IBlackjackService blackjackService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /blackjack/stand с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                BlackjackGameState blackjackGameState = await blackjackService.Stand(userId);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();

        app.MapPost("/blackjack/endGame", async (HttpContext httpContext, IBlackjackService blackjackService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /spinRoulette с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                BlackjackGameResult blackjackGameState = await blackjackService.EndGame(userId);
                return Results.Ok(blackjackGameState);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();
    }
}