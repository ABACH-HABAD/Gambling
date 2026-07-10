using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Core.Models;
using Gambling.WebApi.Classes;

namespace Gambling.WebApi.Endpoints;

public static class GameDataEndpoints
{
    public static void MapGameDataEndpoints(this WebApplication app)
    {
        app.MapGet("/getGames", async (HttpContext httpContext, int gameType, IGameService gameService) =>
        {
            try
            {
                List<GameModel> games = await gameService.GetAllGamesAsync((GameType)gameType);
                return Results.Ok(games);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization(politics => politics.RequireRole(AdminRole.Id));
    }
}