using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;
using System.Text.Json;

namespace DirectoryService.Presentation.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");
        context.Response.ContentType = "application/json";
        (int code, Error[]? errors) = exception switch
        {
            BadRequestException => (StatusCodes.Status500InternalServerError, JsonSerializer.Deserialize<Error[]>(exception.Message)),
            NotFoundException => (StatusCodes.Status404NotFound, JsonSerializer.Deserialize<Error[]>(exception.Message)),
            _ => (StatusCodes.Status510NotExtended, new Error[] { Error.Failure("internal.server.error", "An internal server error occurred.") })
        };

        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(errors);
    }
}

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}