using PrintService.Application.Interfaces.IServices;
using PrintService.Application.Services;
using PrintService.Infraestructure.Services;

namespace PrintService.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services) 
    { 
        services.AddSignalR();
        services.AddScoped<INotificationService, SignalRNotificationService>();
        services.AddScoped<IJobService, JobService>();

        return services;
    }
}