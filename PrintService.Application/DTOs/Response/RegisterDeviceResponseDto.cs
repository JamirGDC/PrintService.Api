namespace PrintService.Application.DTOs.Response;

public class RegisterDeviceResponseDto
{
    public Guid DeviceId { get; set; } = default!;
    public string MachineName { get; set; } = default!;
    public string AgentRegion { get; set; } = default!;
    public DateTime LastSeenUtc { get; set; }
}