using PrintService.Domain.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace PrintService.Filters;

public class ResultHttpCodeFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is ResultBase resultWithStatus)
        {
            context.HttpContext.Response.StatusCode = (int)resultWithStatus.HttpStatusCode;
            return new JsonResult(resultWithStatus)
            {
                StatusCode = (int)resultWithStatus.HttpStatusCode,
                ContentType = "application/json"
            };
        }
        return result;
    }
}