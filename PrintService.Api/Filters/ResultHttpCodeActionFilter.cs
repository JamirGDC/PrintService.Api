using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PrintService.Domain.Common.Result;

namespace PrintService.Filters;

public class ResultHttpCodeActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult &&
            objectResult.Value is ResultBase resultWithStatus)
        {
            objectResult.StatusCode = (int)resultWithStatus.HttpStatusCode;
        }
        else if (context.Result is ResultBase resultDirect)
        {
            context.Result = new ObjectResult(resultDirect)
            {
                StatusCode = (int)resultDirect.HttpStatusCode
            };
        }
    }
}