using Gambling.Core.Exceptions;

namespace Gambling.WebApi.Middleware;

public class ErrorHandlerMiddleware(RequestDelegate next) : BaseMiddleware(next)
{
    public override async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (YouDoNotHavePermissionException)
        {
            context.Response.StatusCode = 403;
        }
        catch (InsufficientRightsException)
        {
            context.Response.StatusCode = 403;
        }
        catch (ValidationException)
        {
            context.Response.StatusCode = 400;
        }
        catch (ServiceNotFoundException)
        {
            context.Response.StatusCode = 500;
        }
        catch (AccountNotFoundException)
        {
            context.Response.StatusCode = 401;
        }
        catch (SessionNotFoundException)
        {
            context.Response.StatusCode = 401;
        }
        catch (NoConnectionException)
        {
            context.Response.StatusCode = 500;
        }
        catch (CannotFindConnectionString)
        {
            context.Response.StatusCode = 500;
        }
        catch (DataBaseException)
        {
            context.Response.StatusCode = 500;
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}