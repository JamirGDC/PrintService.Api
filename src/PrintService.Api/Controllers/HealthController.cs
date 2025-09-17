namespace PrintService.Api.Controllers;

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

[ApiController]
[Route("")]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public HealthController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }


    [HttpGet("healthz")]
    public IActionResult Healthz()
    {
        return Ok("ok");
    }


    [HttpGet("readyz")]
    public async Task Readyz()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        Response.ContentType = "application/json";
        await UIResponseWriter.WriteHealthCheckUIResponse(HttpContext, report);
    }

}