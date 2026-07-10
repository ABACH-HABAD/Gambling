using Gambling.Application.Core.Api.Requests;
using Gambling.Core.Models;
using MySqlX.XDevAPI.Common;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Windows.System;

namespace Gambling.WebApi.Classes;

public class UserIdCheckerService : IIdCheckerService
{
    public bool GetUserIdFormContext(HttpContext httpContext, [MaybeNullWhen(false)] out int userId, [MaybeNullWhen(true)] out IResult result)
    {
        Claim? userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            result = Results.Unauthorized();
            userId = 0;
            return false;
        }

        userId = int.Parse(userIdClaim.Value);
        result = null;
        return true;
    }

    public bool CheckUserId(HttpContext httpContext, ref int userId, [MaybeNullWhen(true)] out IResult result)
    {
        Claim? userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            result = Results.Unauthorized();
            return false;
        }

        if (userId == 0) userId = int.Parse(userIdClaim.Value);
        else if (int.Parse(userIdClaim.Value) != userId)
        {
            Claim? userRoleClaim = httpContext.User.FindFirst(ClaimTypes.Role);
            if (userRoleClaim is null)
            {
                result = Results.Unauthorized();
                return false;
            }

            if (userRoleClaim.Value != AdminRole.Id)
            {
                result = Results.Forbid();
                return false;
            }
        }

        result = null;
        return true;
    }

    public bool GetUserIdFormRequest(HttpContext httpContext, BaseRequiringIdRequest request, [MaybeNullWhen(false)] out int userId, [MaybeNullWhen(true)] out IResult result)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            userId = 0;
            result = Results.Unauthorized();
            return false;
        }

        if (request.UserId == 0) userId = int.Parse(userIdClaim.Value);
        else if (int.Parse(userIdClaim.Value) != request.UserId)
        {
            var userRoleClaim = httpContext.User.FindFirst(ClaimTypes.Role);
            if (userRoleClaim is null)
            {
                userId = 0;
                result = Results.Unauthorized();
                return false;
            }

            if (userRoleClaim.Value != AdminRole.Id)
            {
                userId = 0;
                result = Results.Forbid();
                return false;
            }
        }

        userId = request.UserId;
        result = null;
        return true;
    }

    public bool GetUserIdFormUserModel(HttpContext httpContext, UserModel userModel, [MaybeNullWhen(false)] out int userId, [MaybeNullWhen(true)] out IResult result)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            userId = 0;
            result = Results.Unauthorized();
            return false;
        }

        if (userModel.Id == 0) userId = int.Parse(userIdClaim.Value);
        else if (int.Parse(userIdClaim.Value) != userModel.Id)
        {
            var userRoleClaim = httpContext.User.FindFirst(ClaimTypes.Role);
            if (userRoleClaim is null)
            {
                userId = 0;
                result = Results.Unauthorized();
                return false;
            }

            if (userRoleClaim.Value != AdminRole.Id)
            {
                userId = 0;
                result = Results.Forbid();
                return false;
            }
        }

        userId = userModel.Id;
        result = null;
        return true;
    }
}