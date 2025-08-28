namespace PrintService.Api.Models;

public class PrintJob
{
    public string JobId { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public string? PrinterKey { get; set; }
    public string ContentType { get; set; } = "text/plain";
    public string Payload { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}