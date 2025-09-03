using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Domain.Entities;
using System.Text.Json;

namespace PrintService.Application.Utilities;

public static class DeviceMappers
{
    public static Device ToDomain(this RegisterDeviceRequestDto registerDevice)
    {
        return new Device
        {
            Id = registerDevice.DeviceId,
            MachineName = registerDevice.MachineName,
            AgentRegion = registerDevice.AgentRegion,
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