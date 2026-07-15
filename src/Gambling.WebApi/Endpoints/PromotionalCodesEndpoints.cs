using Gambling.Application.Core.Abstractions.PromtionCodes;
using Gambling.Core.Models;
using Gambling.WebApi.Classes;

namespace Gambling.WebApi.Endpoints;

public static class PromotionalCodesEndpoints
{
    public static void MapPromotionalCodes(this WebApplication app)
    {
        app.MapPost("/promocode/add", async (HttpContext context, PromotionalCodeModel code, IPromotionalCodeService promotionalCodeService) =>
        {
            try
            {
                await promotionalCodeService.AddPromocodeAsync(code.Code, code.Use, interestBonus: code.InterestBonus, quantitativeBonus: code.QuantitativeBonus, freeSpins: code.FreeSpins);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }).RequireAuthorization(politics => politics.RequireRole(AdminRole.Id));

        app.MapGet("/promocode/getAll", async (HttpContext context, IPromotionalCodeService promotionalCodeService) =>
        {
            try
            {
                List<PromotionalCodeModel> codelist = await promotionalCodeService.GetPromocodeListAsync();
                return Results.Ok(codelist);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }).RequireAuthorization(politics => politics.RequireRole(AdminRole.Id));
    }
}