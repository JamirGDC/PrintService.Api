namespace PrintService.Application.DTOs.Response;

public class CreateJobResponseDto
{
    public Guid JobId { get; set; }
    public DateTime CreatedUtc { get; set; }
}