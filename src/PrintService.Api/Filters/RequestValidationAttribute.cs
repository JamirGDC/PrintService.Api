using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PrintService.Application.Interfaces.Services;
using System.Net;
using System.Text.Json;

namespace PrintService.Api.Filters;

public abstract class RequestValidationAttribute : ActionFilterAttribute
{
    protected bool TryGetRegion(HttpContext context, out string? region, out IActionResult? errorResult)
    {
        if (!context.Request.Headers.TryGetValue("x-region", out var value))
        {
            errorResult = new JsonResult(new { error = "Missing x-region header" })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            region = null;
            return false;
        }

        region = value!;
        errorResult = null;
        return true;
    }

    protected bool TryGetIdempotencyKey(HttpContext context, out Guid key, out IActionResult? errorResult)
    {
        if (!context.Request.Headers.TryGetValue("x-idempotency-key", out var value))
        {
            errorResult = new JsonResult(new { error = "Missing x-idempotency-key header" })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            key = Guid.Empty;
            return false;
        }

        if (!Guid.TryParse(value, out key))
        {
            errorResult = new JsonResult(new { error = "Invalid x-idempotency-key" })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            return false;
        }

        errorResult = null;
        return true;
    }

    protected void FillRequestContext(HttpContext context, string region, Guid idempotencyKey)
    {
        var requestContext = context.RequestServices.GetRequiredService<IRequestContext>();
        requestContext.Region = region;
        requestContext.CallerId = context.User?.Identity?.Name ?? "unknown";
        requestContext.IdempotencyKey = idempotencyKey;
    }
}