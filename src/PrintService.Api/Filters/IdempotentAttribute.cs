using System.Net;
using Microsoft.AspNetCore.Http;
namespace PrintService.Api.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.DTOs.Response;
using Application.Interfaces.Services;
using Infraestructure.Data;
using Shared.Result;

[AttributeUsage(AttributeTargets.Method)]
public class IdempotentAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
        var requestContext = context.HttpContext.RequestServices.GetRequiredService<IRequestContext>();

        var callerId = requestContext.CallerId;
        var idempotencyKey = requestContext.IdempotencyKey;

        if (string.IsNullOrEmpty(callerId) || idempotencyKey == Guid.Empty)
        {
            await next();
            return;
        }

        var existing = await dbContext.IdempotencyKeys.FindAsync(new object[] { callerId, idempotencyKey.ToString() });

        if (existing != null)
        {
            var response = Result<CreateJobResponseDto>.Failure(HttpStatusCode.Conflict).WithPayload(new CreateJobResponseDto
            {
                JobId = existing.JobId
            });

            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }

        await next();
    }
}
