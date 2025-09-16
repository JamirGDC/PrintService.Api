namespace PrintService.Application.DTOs.Response;

public class AcknowledgeJobResponseDto
{
    public Guid JobId { get; set; }
    public string Status { get; set; } = default!;
    public DateTime? AcknowledgedAt { get; set; }
}