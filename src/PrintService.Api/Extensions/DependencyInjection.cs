namespace PrintService.Api.Extensions;

using Microsoft.OpenApi.Models;
using Filters;
using Hubs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services;
using Infraestructure.Context;
using Infraestructure.Security;
using Infraestructure.SignalR;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services)
    {
        // Controllers
        services.AddControllers(options =>
        {
            options.Filters.Add<ResultHttpCodeActionFilter>();
            options.Filters.Add<LoggingFilters>();
        });

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // Auth
        services.AddJwtAuthentication(services.BuildServiceProvider().GetRequiredService<IConfiguration>());
        services.AddScopePolicies();

        // SignalR
        services.AddSignalR();
        services.AddScoped<INotificationHubContext, PrintHubContext>();
        services.AddScoped<INotificationService, SignalRNotificationService>();



        //Application Services
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDeviceService, DeviceService>();
        services.AddScoped<IRequestContext, RequestContext>();
        services.AddScoped<IJwtToken, JwtToken>();

        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.SetIsOriginAllowed(_ => true)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        return services;
    }
}