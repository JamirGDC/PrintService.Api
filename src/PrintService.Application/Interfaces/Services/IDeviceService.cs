using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Shared.Result;

namespace PrintService.Application.Interfaces.Services;

public interface IDeviceService
{
    Task<Result<RegisterDeviceResponseDto>> RegisterDeviceAsync(RegisterDeviceRequestDto registerDevice, CancellationToken cancellationToken);
}