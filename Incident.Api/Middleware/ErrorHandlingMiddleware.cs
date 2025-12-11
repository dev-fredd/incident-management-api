using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Incident.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error during request");

            await HandleError(context, ex);
        }
    }

    private static async Task HandleError(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var status = ex switch
        {

            DbUpdateConcurrencyException => HttpStatusCode.Conflict,

            DbUpdateException => HttpStatusCode.BadRequest,

            ArgumentException => HttpStatusCode.BadRequest,

            KeyNotFoundException => HttpStatusCode.NotFound,

            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)status;

        var json = JsonSerializer.Serialize(new
        {
            error = ex.Message,
            type = ex.GetType().Name,
            status = (int)status,
            traceId = context.TraceIdentifier
        });

        await context.Response.WriteAsync(json);
    }
}
