using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.Api.Requests;
using Gambling.Application.Core.Api.Results;
using Gambling.WebApi.Classes.Services;

namespace Gambling.WebApi.Endpoints;

public static class SlotsEndpoins
{
    public static void MapSlotsEndpoints(this WebApplication app)
    {
        app.MapPost("/spinSlots", async (HttpContext httpContext, SlotSpinRequest slotSpinRequest, ISlotsService slotService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, slotSpinRequest, out int userId, out IResult? result)) return result;

            try
            {
                SlotGameResult? slotGameResult = await slotService.SpinAsync(userId, slotSpinRequest.Bid, slotSpinRequest.LinesCount, slotSpinRequest.ColumnsCount);
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