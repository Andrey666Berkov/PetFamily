using PetFamily.Shared.Core.Controllers;
using PetFamily.Shared.SharedKernel;

namespace PetFamily.Web.Middlewares;

public class ExeptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExeptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); //вызов всего приложения
        }
        catch (Exception ex)
        {
            var responseError=ErrorMy.Failure("server.internal", ex.Message);
            var envelope = Envelope.Error(responseError);
            
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode=StatusCodes.Status500InternalServerError;
            
            await httpContext.Response.WriteAsJsonAsync(envelope);
        }
    }
}

public static class ExeptionMiddlewareExtansion
{
    public static IApplicationBuilder UseExeptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExeptionMiddleware>();
    }
}