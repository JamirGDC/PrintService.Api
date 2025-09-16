using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace PrintService.Api.Telemetry;

public static class OpenTelemetry
{
    private static string? _openTelemetryUrl;

    public static void AddTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        var otlpEndpoint = configuration["OpenTelemetry:Endpoint"];

        services.AddOpenTelemetry()
            .ConfigureResource(r =>
                r.AddService(configuration["AppName"] ?? "PrintService"))
            .WithTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(o => o.Endpoint = new Uri(otlpEndpoint));
            })
            .WithMetrics(meterProviderBuilder =>
            {
                meterProviderBuilder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(o => o.Endpoint = new Uri(otlpEndpoint));
            });
    }

    public static void AddLogging(this IHostBuilder builder, IConfiguration configuration)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddOpenTelemetry(options =>
            {
                options.IncludeFormattedMessage = true;
                options.SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(configuration["AppName"] ?? "PrintService"));
                options.AddConsoleExporter();
            });
        });
    }
}