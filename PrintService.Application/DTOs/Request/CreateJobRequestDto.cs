namespace PrintService.Application.DTOs.Request;

public class CreateJobRequestDto
{
    public string UserId { get; set; } = default!;
    public string PrinterKey { get; set; }
    public string ContentType { get; set; } = default!;
    public string Payload { get; set; } = default!;
}