using System.Security.Claims;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Game;
using BusinessLogic.Game.Roulette;

namespace GamblingWebApi.Endpoints;

public static class GameDataEndpoints
{
    public static void MapGameDataEndpoints(this WebApplication app)
    {
        app.MapGet("/getGames", async (HttpContext httpContext, int gameType, IGameService gameService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /spinRoulette с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Json(new RouletteGameResult(false, "Токен сессии недействителен или остутствует", 0, 0), statusCode: StatusCodes.Status400BadRequest, contentType: "application/json");
            var userStatusClaim = httpContext.User.FindFirst(ClaimTypes.Role);
            if (userStatusClaim == null || int.Parse(userStatusClaim.Value) != 3)
                return Results.Forbid();

            int adminId = int.Parse(userIdClaim.Value);

            try
            {
                return Results.Ok(await gameService.GetAllGamesAsync(adminId, (GameType)gameType));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();
    }
}
