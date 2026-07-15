using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Api.Requests;
using Gambling.Core.Models;
using Gambling.WebApi.Classes;
using Gambling.WebApi.Classes.Services;

namespace Gambling.WebApi.Endpoints;

public static class UserDataEndpoints
{
    public static void MapUserDataEndpoints(this WebApplication app)
    {
        app.MapGet("/userData", async (HttpContext httpContext, int userId, IAccountDataService accountDataService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;

            UserModel? user = await accountDataService.GetUserDataAsync(userId);
            if (user != null) return Results.Ok(user);
            else return Results.BadRequest();
        }).RequireAuthorization();

        app.MapGet("/getAllUsers", async (HttpContext httpContext, IAccountDataService accountDataService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormContext(httpContext, out int userId, out IResult? httpResult)) return httpResult;

            try
            {
                List<UserModel> userList = await accountDataService.GetAllUsersAsync();
                return Results.Ok(userList);
            }
            catch
            {
                return Results.BadRequest();
            }
        }).RequireAuthorization(policy => policy.RequireRole(AdminRole.Id));

        app.MapPut("/changeName", async (HttpContext httpContext, ChangeNameRequest request, IAccountDataService accountDataService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, request, out int userId, out IResult? httpResult)) return httpResult;

            try
            {
                bool result = await accountDataService.ChangeNameAsync(userId, request.Name);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest();
            }

        });

        app.MapPut("/changeBalance", async (HttpContext httpContext, ChangeBalanceRequest request, IAccountDataService accountDataService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, request, out int userId, out IResult? httpResult)) return httpResult;

            try
            {
                bool result = await accountDataService.ChangeBalanceAsync(request.UserId, request.Sum);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization(policy => policy.RequireRole(AdminRole.Id));

        app.MapPut("/changeStatus", async (HttpContext httpContext, ChangeStatusRequest request, IAccountDataService accountDataService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, request, out int userId, out IResult? httpResult)) return httpResult;

            try
            {
                bool result = await accountDataService.ChangeStatusAsync(request.UserId, request.StatusId);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization(policy => policy.RequireRole(AdminRole.Id));

        app.MapPut("/block", async (HttpContext httpContext, BlockRequest request, IAccountDataService accountDataService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, request, out int userId, out IResult? httpResult)) return httpResult;

            try
            {
                bool result = await accountDataService.BlockUser(request.UserId);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization(policy => policy.RequireRole(AdminRole.Id));

        app.MapPut("/unblock", async (HttpContext httpContext, UnblockRequest request, IAccountDataService accountDataService, IIdCheckerService idChecker) =>
        {
            if (!idChecker.GetUserIdFormRequest(httpContext, request, out int userId, out IResult? httpResult)) return httpResult;

            try
            {
                bool result = await accountDataService.UnblockUser(request.UserId);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest();
            }

        }).RequireAuthorization(policy => policy.RequireRole(AdminRole.Id));
    }
}