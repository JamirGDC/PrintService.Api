using PrintService.Api.Extensions;
using PrintService.Api.Hubs;
using PrintService.Api.Middleware;
using PrintService.Api.Telemetry;
using PrintService.Infraestructure.Data;
using PrintService.Infraestructure.Security;
using PrintService.Shared.Logging;


var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.ConfigureSerilog();
builder.Host.AddLogging(builder.Configuration);

// API + Servicios
builder.Services.AddApiDependencies();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddTelemetry(builder.Configuration);

// HealthChecks
builder.Services.AddHealthChecks();
builder.Services.AddMSSQLHealthCheck(async provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return configuration.GetConnectionString("DefaultConnection")!;
});
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

var app = builder.Build();

// Middleware y endpoints
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LoggingEnrichmentMiddleware>();

app.MapControllers();
app.UseHealthChecksUI(config => config.UIPath = "/healthz-ui");
app.MapHub<PrintHub>("/hubs/print");

app.Run();


