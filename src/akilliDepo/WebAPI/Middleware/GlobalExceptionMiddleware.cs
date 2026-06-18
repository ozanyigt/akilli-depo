using System.Text.Json;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;

namespace WebAPI.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode;
        string message;

        switch (exception)
        {
            case BusinessException businessException:
                statusCode = StatusCodes.Status400BadRequest;
                message = businessException.Message;
                break;
            case AuthorizationException authorizationException:
                statusCode = StatusCodes.Status403Forbidden;
                message = authorizationException.Message;
                break;
            case NotFoundException notFoundException:
                statusCode = StatusCodes.Status404NotFound;
                message = notFoundException.Message;
                break;
            case ValidationException validationException:
                statusCode = StatusCodes.Status400BadRequest;
                message = validationException.Message;
                break;
            case ConflictException conflictException:
                statusCode = StatusCodes.Status409Conflict;
                message = conflictException.Message;
                break;
            default:
                _logger.LogError(exception, "Unhandled exception");
                statusCode = StatusCodes.Status500InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(new { statusCode, message })
        );
    }
}

public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<GlobalExceptionMiddleware>();
}
