namespace Gambling.WebApi.Classes;

public static class TestModeExtencions
{
    public static IWebHostBuilder UseTestMode(this IWebHostBuilder builder)
    {
        ClaimsPrincipalExtensions.TestMode = true;

        return builder;
    }
}