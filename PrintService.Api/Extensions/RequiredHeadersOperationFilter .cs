using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PrintService.Api.Extensions;

public class RequiredHeadersOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var path = context.ApiDescription.RelativePath;
        if (path != null &&
            (path.StartsWith("connect/token") ||
             path.StartsWith("healthz") ||
             path.StartsWith("readyz")))
        {
            return;
        }

        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "x-region",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema { Type = "string" },
            Description = "Region code (e.g. DEU, ITA)"
        });

        //operation.Parameters.Add(new OpenApiParameter
        //{
        //    Name = "x-idempotency-key",
        //    In = ParameterLocation.Header,
        //    Required = true,
        //    Schema = new OpenApiSchema { Type = "string", Format = "uuid" },
        //    Description = "Idempotency key (must be a valid GUID)"
        //});
    }
}