using Serilog.Events;

namespace PrintService.Shared.Logging;

public class ConsoleLoggerConfiguration
{
    public bool Enabled { get; set; } = false;
    public LogEventLevel MinimumLevel { get; set; }
}