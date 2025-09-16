using Microsoft.AspNetCore.Http;
using PrintService.Application.Interfaces.Services;
using PrintService.Infraestructure.Data;
using System.Text.Json;

namespace PrintService.Infraestructure.Extensions.Middleware;

public class IdempotencyMiddleware
{
    private readonly RequestDelegate _next;

    public IdempotencyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private static readonly HashSet<PathString> _idempotentEndpoints = new()
    {
        new PathString("/v1/jobs")
    };

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext, IRequestContext requestContext)
    {
        if (context.Request.Method != HttpMethods.Post ||
            !_idempotentEndpoints.Contains(context.Request.Path))
        {
            await _next(context);
            return;
        }

        var callerId = requestContext.CallerId;
        var idempotencyKey = requestContext.IdempotencyKey;

        var existing = await dbContext.IdempotencyKeys.FindAsync(new object[] { callerId, idempotencyKey.ToString() });

        if (existing != null)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "application/json";

            var payload = new
            {
                jobId = existing.JobId
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
            return;
        }

        await _next(context);

    }
}
