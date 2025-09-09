using PrintService.Api.Hubs;
using PrintService.Application.Interfaces.IServices;
using PrintService.Application.Services;
using PrintService.Infraestructure.Context;

namespace PrintService.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services) 
    { 
        services.AddSignalR();
        services.AddScoped<INotificationService, SignalRNotificationService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDeviceService, DeviceService>();
        services.AddScoped<IRequestContext, RequestContext>();



        return services;
    }
}