using System.Security.Claims;
using DataBaseClasses.Entity;
using BusinessLogic.Account;

namespace GamblingWebApi.Endpoints;

public static class UserDataEndpoints
{
    public static void MapUserDataEndpoints(this WebApplication app)
    {
        app.MapGet("/userData", async (HttpContext httpContext, IAccountDataService accountDataService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"Получен запрос /getUserData с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            User? user = await accountDataService.GetUserDataAsync(userId);
            if (user != null) return Results.Ok(user);
            else return Results.BadRequest();
        }).RequireAuthorization();

        app.MapPut("/userData", async (User userData, HttpContext httpContext, IAccountDataService accountDataService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"Получен запрос /putUserData: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();

            userData.Id = int.Parse(userIdClaim.Value);

            User? user = await accountDataService.UpdateUserDataAsync(userData);
            if (user != null) return Results.Ok(user);
            else return Results.BadRequest();
        }).RequireAuthorization();

        app.MapGet("/getAllUsers", async (HttpContext httpContext, IAccountDataService accountDataService) =>
        {
            string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"Получен запрос /getAllUsers с IP: {clientIp}");

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Results.Unauthorized();
            var userStatusClaim = httpContext.User.FindFirst(ClaimTypes.Role);
            if (userStatusClaim == null || int.Parse(userStatusClaim.Value) != 3)
                return Results.Forbid();

            int adminId = int.Parse(userIdClaim.Value);

            try
            {
                List<User> userList = await accountDataService.GetAllUsersAsync(adminId);
                return Results.Ok(userList);
            }
            catch
            {
                return Results.BadRequest();
            }
        }).RequireAuthorization();
    }
}
