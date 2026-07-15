using Gambling.WebApi.Classes;
using Gambling.WebApi.Classes.Decorators;

namespace Gambling.WebApi.Middleware;

public class ConsoleLoggerMiddleware(RequestDelegate next) : BaseMiddleware(next)
{
    public override async Task InvokeAsync(HttpContext context)
    {
        ConsoleDecorators.RequesteDecoratorAsync(context.Request.Path, context.Request.Method, context.GetIp());
        await _next.Invoke(context);
        ConsoleDecorators.ResponceDecoratorAsync(context.Request.Path, context.Request.Method, context.GetIp(), context.Response.StatusCode);
    }
}