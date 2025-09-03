namespace PrintService.Api.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddScopePolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("print.jobs.read", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "print.jobs.read");
            });

            options.AddPolicy("print.jobs.write", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "print.jobs.write");
            });

            options.AddPolicy("print.jobs.ack", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "print.jobs.ack");
            });
        });

        return services;
    }
}