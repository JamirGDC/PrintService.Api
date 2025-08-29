using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrintService.Application.Interfaces;
using PrintService.Infraestructure.Services;
using System;
using PrintService.Application.Interfaces.IRepositories;
using PrintService.Infraestructure.Repositories;

namespace PrintService.Infraestructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IPrintJobRepository, PrintJobRepository>();

        return services;
    }
}