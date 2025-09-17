namespace PrintService.Api.Extensions;

using PrintService.Domain.Enums;
using System.ComponentModel;


public static class AuthorizationExtensions
{
    public static IServiceCollection AddScopePolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            foreach (var scope in Enum.GetValues<Scopes>())
            {
                var scopeName = ((Scopes)scope).GetDescription();

                options.AddPolicy(scopeName, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", scopeName);
                });
            }
        });

        return services;
    }

    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault() as DescriptionAttribute;
        return attribute?.Description ?? value.ToString();
    }
}
