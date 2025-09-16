using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace PrintService.Shared.Logging;

public static class ConfigureLogger
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
        => builder.UseSerilog((context, LoggerConfiguration)
            => ConfigureSerilogLogger( LoggerConfiguration, context.Configuration));

    public static LoggerConfiguration ConfigureSerilogLogger(LoggerConfiguration loggerConfiguration,
        IConfiguration configuration)
    {
        ConsoleLoggerConfiguration consoleLogger = new ConsoleLoggerConfiguration();
        configuration.GetSection("Logging:Console").Bind(consoleLogger);

        FileLoggerConfiguration fileLogger = new FileLoggerConfiguration();
        configuration.GetSection("Logging:File").Bind(fileLogger);

        return loggerConfiguration
            .Enrich.FromLogContext()
            .AddConsoleLogger(consoleLogger)
            .AddFileLogger(fileLogger);
        
    }

}