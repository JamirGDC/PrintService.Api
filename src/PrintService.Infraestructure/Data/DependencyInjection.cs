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
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }

    public static IServiceCollection AddMSSQLHealthCheck(this IServiceCollection serviceCollection,
        Func<IServiceProvider, Task<string>> connectionString)
    {
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        string mssqlConnectionString = connectionString.Invoke(serviceProvider).Result;
        serviceCollection.AddHealthChecks().AddSqlServer(mssqlConnectionString);
        return serviceCollection;
    }
}