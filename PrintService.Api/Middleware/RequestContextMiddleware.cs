using PrintService.Infraestructure.Context;

namespace PrintService.Api.Middleware;

public class RequestContextMiddleware
{
    private readonly RequestDelegate _next;

    public RequestContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, RequestContext requestContext)
    {
        if (!context.Request.Headers.TryGetValue("x-region", out var region))
            throw new BadHttpRequestException("Missing x-region header");

        if (!context.Request.Headers.TryGetValue("Idempotency-Key", out var idempotencyKey))
            throw new BadHttpRequestException("Missing Idempotency-Key header");

        if (!Guid.TryParse(idempotencyKey, out _))
            throw new BadHttpRequestException("Invalid Idempotency-Key");

        requestContext.Region = region!;
        requestContext.IdempotencyKey = idempotencyKey!;

        await _next(context);
    }
}
