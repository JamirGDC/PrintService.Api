using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Domain.Common.Result;

namespace PrintService.Application.Interfaces.IServices;

public interface IDeviceService
{
    Task<Result<RegisterDeviceResponseDto>> RegisterDeviceAsync(RegisterDeviceRequestDto registerDevice, CancellationToken cancellationToken);
}