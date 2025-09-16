namespace PrintService.Application.DTOs.Request;

public class RegisterDeviceRequestDto
{
    public Guid DeviceId { get; set; }
    public string MachineName { get; set; }
    public string AgentRegion { get; set; }
    public List<string> Printers { get; set; } = new();
    public List<string> Capabilities { get; set; } = new();
}