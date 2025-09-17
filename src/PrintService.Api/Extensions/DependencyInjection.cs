using Microsoft.OpenApi.Models;
using PrintService.Api.Filters;
using PrintService.Api.Hubs;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.Services;
using PrintService.Application.Services;
using PrintService.Infraestructure.Context;
using PrintService.Infraestructure.Security;

namespace PrintService.Api.Extensions;

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