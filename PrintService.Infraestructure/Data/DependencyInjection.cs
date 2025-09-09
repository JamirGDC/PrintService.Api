using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrintService.Application.Interfaces;
using System;
using PrintService.Infraestructure.Repositories;
using PrintService.Application.Interfaces.Repositories;

namespace PrintService.Infraestructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IPrintJobRepository, PrintJobRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        return services;
    }
}