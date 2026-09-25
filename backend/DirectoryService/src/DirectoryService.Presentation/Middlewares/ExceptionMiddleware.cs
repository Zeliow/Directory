using DirectoryService.Shared;

namespace DirectoryService.Presentation.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException exception)
        {
            _logger.LogWarning(exception, "Application error: {Code}", exception.Errors.FirstOrDefault()?.Code);
            await WriteErrorsAsync(context, exception.Errors);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred.");
            await WriteErrorsAsync(context, [Error.Failure("internal.server.error", "An internal server error occurred.")]);
        }
    }

    private static async Task WriteErrorsAsync(HttpContext context, IReadOnlyCollection<Error> errors)
    {
        if (context.Response.HasStarted)
        {
            return;
        }

        IReadOnlyCollection<Error> errorList = errors.Count > 0
            ? errors
            : [Error.Failure("internal.server.error", "An internal server error occurred.")];

        var errorTypes = errorList.Select(error => error.Type).Distinct().ToList();
        context.Response.StatusCode = errorTypes.Count == 1
            ? MapStatusCode(errorTypes[0])
            : StatusCodes.Status500InternalServerError;

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(errorList);
    }

    private static int MapStatusCode(ErrorType type) => type switch
    {
        ErrorType.VALIDATION => StatusCodes.Status400BadRequest,
        ErrorType.NOT_FOUND => StatusCodes.Status404NotFound,
        ErrorType.CONFLICT => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError,
    };
}

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}
