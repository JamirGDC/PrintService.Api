using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Domain.Entities;
using System.Text.Json;

namespace PrintService.Application.Utilities.Mappers;

public static class DeviceMappers
{
    public static Device ToDomain(this RegisterDeviceRequestDto registerDevice, string region)
    {
        return new Device
        {
            Id = registerDevice.DeviceId,
            MachineName = registerDevice.MachineName,
            AgentRegion = region,
            PrintersJson = JsonSerializer.Serialize(registerDevice.Printers),
            LastSeenUtc = DateTime.UtcNow
        };
    }

    public static RegisterDeviceResponseDto ToResponse(this Device device)
    {
        return new RegisterDeviceResponseDto
        {
           DeviceId = device.Id
        };
    }
}