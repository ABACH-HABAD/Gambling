using System.Security.Claims;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Game.Slots;

namespace GamblingWebApi.Endpoints;

public static class SlotsEndpoins
{
    public static void MapSlotsEndpoints(this WebApplication app)
    {
        app.MapPost("/spinSlots", async (HttpContext httpContext, SlotSpinRequest slotSpinRequest, ISlotsService slotService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            Console.WriteLine($"Получен запрос /spinSlots с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Json(new SlotGameResult(false, "Токен сессии недействителен или остутствует", 0, null!), statusCode: StatusCodes.Status400BadRequest, contentType: "application/json");

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                SlotGameResult? slotGameResult = await slotService.Spin(userId, slotSpinRequest.Bid, slotSpinRequest.LinesCount, slotSpinRequest.ColumnsCount);
                if (slotGameResult != null) return Results.Ok(slotGameResult);
                else return Results.Json(slotGameResult, statusCode: StatusCodes.Status400BadRequest, contentType: "application/json");

            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }).RequireAuthorization();
    }
}
