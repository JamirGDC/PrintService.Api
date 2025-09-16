namespace PrintService.Application.DTOs.Response;

public class JobDetailResponseDto
{
    public Guid JobId { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string? Payload { get; set; }
    public string PrinterKey { get; set; } = string.Empty;
    public string HmacKeyId { get; set; } = string.Empty;
    public string HmacSignature { get; set; } = string.Empty;
}