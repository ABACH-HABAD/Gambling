using System.Security.Claims;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Game.Roulette;

namespace GamblingWebApi.Endpoints;

public static class RouletteEndpoints
{
    public static void MapRouletteEndpoints(this WebApplication app)
    {
        app.MapPost("/spinRoulette", async (HttpContext httpContext, RouletteSpinRequest rouletteSpinRequest, IRouletteService rouletteService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /spinRoulette с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Json(new RouletteGameResult(false, "Токен сессии недействителен или остутствует", 0, 0), statusCode: StatusCodes.Status401Unauthorized, contentType: "application/json");

            int userId = int.Parse(userIdClaim.Value);

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
