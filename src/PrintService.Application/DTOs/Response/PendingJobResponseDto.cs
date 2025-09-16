namespace PrintService.Application.DTOs.Response;

public class PendingJobResponseDto
{
    public Guid JobId { get; set; }
    public DateTime CreatedUtc { get; set; }
}