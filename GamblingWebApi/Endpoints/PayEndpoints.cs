using System.Security.Claims;
using BusinessLogic.Account.Balance;
using BusinessLogic.ApiServices.Requests;

namespace GamblingWebApi.Endpoints;

public static class PayEndpoints
{
    public static void MapPayEndpoints(this WebApplication app)
    {
        app.MapPost("/addToBalace/cardPay", async (HttpContext httpContext, AddToBalanceCardPayRequest request, ICardPayService balanceService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"Получен запрос /addToBalace/cardPay с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            if (await balanceService.AddToBalanceAsync(userId, request.Count, request.Card, request.Promocode)) return Results.Ok();
            else return Results.BadRequest();
        }).RequireAuthorization();

        app.MapPost("/removeFromBalace/cardPay", async (HttpContext httpContext, RemoveFromBalanceCarPayRequest request, ICardPayService balanceService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"Получен запрос /removeFromBalace/cardPay с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            if (await balanceService.RemoveFromBalanceAsync(userId, request.Count, request.Card)) return Results.Ok();
            else return Results.BadRequest();
        }).RequireAuthorization();
    }
}
