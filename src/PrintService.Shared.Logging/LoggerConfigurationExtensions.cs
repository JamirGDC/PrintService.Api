using Serilog;
using Serilog.Formatting.Json;

namespace PrintService.Shared.Logging;

public static class LoggerConfigurationExtensions
{
    public static LoggerConfiguration AddConsoleLogger(this LoggerConfiguration loggerConfiguration,
        ConsoleLoggerConfiguration consoleLoggerConfiguration)
    {
        return consoleLoggerConfiguration.Enabled
            ? loggerConfiguration.WriteTo.Console(formatter: new JsonFormatter(), consoleLoggerConfiguration.MinimumLevel)
            : loggerConfiguration;
    }

    public static LoggerConfiguration AddFileLogger(this LoggerConfiguration loggerConfiguration,
        FileLoggerConfiguration fileLoggerConfiguration)
    {
        return fileLoggerConfiguration.Enabled
            ? loggerConfiguration.WriteTo.File(formatter: new JsonFormatter(), path: fileLoggerConfiguration.Path,
                restrictedToMinimumLevel: fileLoggerConfiguration.MinimumLevel,
                rollingInterval: Enum.TryParse(fileLoggerConfiguration.RollingInterval, out RollingInterval interval)
                    ? interval
                    : RollingInterval.Day
            ) : loggerConfiguration;
    }
}