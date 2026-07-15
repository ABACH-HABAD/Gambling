namespace Gambling.WebApi.Middleware;

public abstract class BaseMiddleware(RequestDelegate next)
{
    protected readonly RequestDelegate _next = next;

    public abstract Task InvokeAsync(HttpContext context);
}