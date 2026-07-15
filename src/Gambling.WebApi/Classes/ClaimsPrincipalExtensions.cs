using System.Security.Claims;

namespace Gambling.WebApi.Classes;

internal static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// TestMode устанавливается на true в тестовых методах чтобы не проверять ip и другие параметры, доступные только при реальных запросах
    /// </summary>
    internal static bool TestMode { get; set; } = false;

    internal static int GetUserId(this ClaimsPrincipal user)
    {
        Claim? userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? throw new Exception("Пользователь не авторизирован");
        return int.Parse(userIdClaim.Value);
    }

    internal static bool TryGetUserId(this ClaimsPrincipal user, out int id)
    {
        id = 0;

        Claim? userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return false;
        else
        {
            id = int.Parse(userIdClaim.Value);
            return true;
        }
    }

    internal static int GetUserId(this HttpContext context)
    {
        ClaimsPrincipal user = context.User;

        Claim? userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? throw new Exception("Пользователь не авторизирован");
        return int.Parse(userIdClaim.Value);
    }

    internal static bool TryGetUserId(this HttpContext context, out int id)
    {
        ClaimsPrincipal user = context.User;

        id = 0;

        Claim? userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return false;
        else
        {
            id = int.Parse(userIdClaim.Value);
            return true;
        }
    }

    internal static string GetIp(this HttpContext context)
    {
        if (TestMode)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? "localhost";
        }
        else return context.Connection.RemoteIpAddress?.ToString() ?? throw new Exception("Неудалось получить ip адрес");
    }
}