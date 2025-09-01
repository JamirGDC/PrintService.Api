namespace PrintService.Application.DTOs.Request;

public class AcknowledgeJobRequestDto
{
    public string Status { get; set; } = default!;
    public int DurationMs { get; set; }
    public string PrinterName { get; set; } = default!;
    public string Driver { get; set; } = default!;
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}