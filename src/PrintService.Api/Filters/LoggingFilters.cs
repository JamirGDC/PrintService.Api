using Microsoft.AspNetCore.Mvc.Filters;
using Serilog.Context;

namespace PrintService.Api.Filters;

public class LoggingFilters : IActionFilter
{
    private static readonly string[] PropertyNames = { "JobId", "DeviceId", "ClientId" };

    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var arg in context.ActionArguments.Values)
        {
            if (arg == null) continue;

            var type = arg.GetType();

            foreach (var propName in PropertyNames)
            {
                var prop = type.GetProperty(propName);
                if (prop != null)
                {
                    var value = prop.GetValue(arg)?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        LogContext.PushProperty(char.ToLowerInvariant(propName[0]) + propName.Substring(1), value);
                    }
                }
            }
        }
    }


    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}