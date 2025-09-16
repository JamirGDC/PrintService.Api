namespace PrintService.Api.Middleware;

public class LoggingEnrichmentMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        //var userId = context.User.Identity?.Name;
        var region = context.Request.Headers["X-Region"].FirstOrDefault();
        var traceId = context.TraceIdentifier;

        //using (Serilog.Context.LogContext.PushProperty("userId", userId))
        using (Serilog.Context.LogContext.PushProperty("region", region))
        using (Serilog.Context.LogContext.PushProperty("traceId", traceId))
        {
            await next(context);
        }
    }
}
