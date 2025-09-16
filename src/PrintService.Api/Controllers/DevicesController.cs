using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces.Services;
using PrintService.Shared.Result;

namespace PrintService.Api.Controllers;

[ApiController]
[Route("v1/devices")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost("register")]
    [Authorize(Policy = "print.jobs.read")]
    public async Task<Result<RegisterDeviceResponseDto>> RegisterDevice([FromHeader(Name = "x-region")] string region, [FromBody] RegisterDeviceRequestDto request, CancellationToken cancellationToken)
    {
        return await _deviceService.RegisterDeviceAsync(request, cancellationToken);
    }
}