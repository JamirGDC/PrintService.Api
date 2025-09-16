using Serilog.Events;

namespace PrintService.Shared.Logging;

public class FileLoggerConfiguration
{
    public bool Enabled { get; set; } = false;
    public string Path { get; set; } = "C:\\Temp\\logs\\PrintService_api-dev-log-.log";
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;
    public string OutputTemplate { get; set; } =
        "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
    public string RollingInterval { get; set; } = "Day";
}