namespace Gambling.WebApi.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ConsoleLoggerMiddleware>();
        builder.UseMiddleware<ErrorHandlerMiddleware>();

        return builder;
    }
}