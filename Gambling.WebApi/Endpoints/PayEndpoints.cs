using Gambling.Application.Core.Abstractions.Balance;
using Gambling.Application.Core.Api.Requests;
using Gambling.WebApi.Classes;

namespace Gambling.WebApi.Endpoints;

public static class PayEndpoints
{
    public static void MapPayEndpoints(this WebApplication app)
    {
        app.MapPost("/addToBalace/cardPay", async (HttpContext httpContext, AddToBalanceCardPayRequest request, ICardPayService balanceService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, request, out int userId, out IResult? result)) return result;

            if (await balanceService.AddToBalanceAsync(userId, request.Count, request.Card, request.Promocode)) return Results.Ok();
            else return Results.BadRequest();
        }).RequireAuthorization();

        app.MapPost("/removeFromBalace/cardPay", async (HttpContext httpContext, RemoveFromBalanceCarPayRequest request, ICardPayService balanceService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, request, out int userId, out IResult? result)) return result;

            if (await balanceService.RemoveFromBalanceAsync(userId, request.Count, request.Card)) return Results.Ok();
            else return Results.BadRequest();
        }).RequireAuthorization();
    }
}