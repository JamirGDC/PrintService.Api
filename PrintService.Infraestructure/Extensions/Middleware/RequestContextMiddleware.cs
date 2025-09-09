using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PrintService.Infraestructure.Context;
using System.Net;
using System.Text.Json;
using PrintService.Application.Interfaces.IServices;

namespace PrintService.Infraestructure.Extensions.Middleware;

public class RequestContextMiddleware
{
    private readonly RequestDelegate _next;

    private static readonly PathString[] _validatedPaths =
    [
        "/v1/jobs"
    ];

    public RequestContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!_validatedPaths.Any(p => context.Request.Path.StartsWithSegments(p)))
        {
            await _next(context);
            return;
        }

        var requestContext = context.RequestServices.GetRequiredService<IRequestContext>();

        if (!context.Request.Headers.TryGetValue("x-region", out var region))
        {
            await WriteErrorResponse(context, HttpStatusCode.BadRequest, "Missing x-region header");
            return;
        }

        //if (!context.Request.Headers.TryGetValue("x-idempotency-key", out var idempotencyKey))
        //{
        //    await WriteErrorResponse(context, HttpStatusCode.BadRequest, "Missing x-idempotency-key header");
        //    return;
        //}

        //if (!Guid.TryParse(idempotencyKey, out _))
        //{
        //    await WriteErrorResponse(context, HttpStatusCode.BadRequest, "Invalid x-idempotency-key");
        //    return;
        //}

        requestContext.Region = region!;
        //requestContext.IdempotencyKey = idempotencyKey!;

        await _next(context);
    }

    private static async Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var error = new
        {
            status = statusCode,
            error = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(error));
    }
}