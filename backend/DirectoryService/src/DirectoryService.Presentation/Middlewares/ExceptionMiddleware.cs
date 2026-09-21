using DirectoryService.Shared;

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

    //to do: refactor this methhod to use expeption on apllication layer and return the error code and message from the exception
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            AppException appException => appException.Error.Type switch
            {
                ErrorType.VALIDATION => StatusCodes.Status400BadRequest,
                ErrorType.NOT_FOUND => StatusCodes.Status404NotFound,
                ErrorType.CONFLICT => StatusCodes.Status409Conflict,
                ErrorType.FAILURE => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            },
            _ => StatusCodes.Status500InternalServerError
        };
        var errorResponse = new
        {
            Code = (exception as AppException)?.Error.Code ?? "internal.server.error",
            Message = exception.Message
        };
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}