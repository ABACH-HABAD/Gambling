using Gambling.Application.Core.Abstractions.Game.Roulette;
using Gambling.Application.Core.Api.Requests;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.Services.Game.Roulette;
using Gambling.WebApi.Classes;

namespace Gambling.WebApi.Endpoints;

public static class RouletteEndpoints
{
    public static void MapRouletteEndpoints(this WebApplication app)
    {
        app.MapPost("/spinRoulette", async (HttpContext httpContext, RouletteSpinRequest rouletteSpinRequest, IRouletteService rouletteService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, rouletteSpinRequest, out int userId, out IResult? result)) return result;

            try
            {
                RouletteGameResult? rouletteGameResult = await rouletteService.Spin(userId, rouletteSpinRequest.Bids.PrepareToJson());
                if (rouletteGameResult != null)
                    return Results.Ok(rouletteGameResult);
                else return Results.Json(rouletteGameResult, statusCode: StatusCodes.Status400BadRequest, contentType: "application/json");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();
    }
}