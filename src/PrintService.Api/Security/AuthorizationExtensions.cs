namespace PrintService.Api.Security;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddScopePolicies(this IServiceCollection services)
    {
        var policyConstants = typeof(AuthorizationPolicies)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(fi => fi.GetRawConstantValue() as string)
            .ToArray();

        services.AddAuthorization(options =>
        {
            foreach (var policyName in policyConstants)
            {
                options.AddPolicy(policyName!, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", policyName!);
                });
            }
        });

        return services;
    }
}