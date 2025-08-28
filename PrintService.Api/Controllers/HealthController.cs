using Microsoft.AspNetCore.Mvc;

namespace PrintService.Api.Controllers;

[ApiController]
[Route("")]
public class HealthController : ControllerBase
{
    [HttpGet("healthz")]
    public IActionResult Health() => Ok("Alive");

    [HttpGet("readyz")]
    public IActionResult Ready()
    {
        return Ok("Ready");
    }
}