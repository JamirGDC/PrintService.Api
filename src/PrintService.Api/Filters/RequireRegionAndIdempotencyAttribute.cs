using Microsoft.AspNetCore.Mvc.Filters;

namespace PrintService.Api.Filters;

public class RequireRegionAndIdempotencyAttribute : RequestValidationAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!TryGetRegion(context.HttpContext, out var region, out var error))
        {
            context.Result = error;
            return;
        }

        if (!TryGetIdempotencyKey(context.HttpContext, out var key, out error))
        {
            context.Result = error;
            return;
        }

        FillRequestContext(context.HttpContext, region!, key);
    }
}

public class RequireRegionAttribute : RequestValidationAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!TryGetRegion(context.HttpContext, out var region, out var error))
        {
            context.Result = error;
            return;
        }

        FillRequestContext(context.HttpContext, region!, Guid.Empty);
    }
}
