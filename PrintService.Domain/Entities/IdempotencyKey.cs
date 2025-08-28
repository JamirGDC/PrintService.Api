namespace PrintService.Domain.Entities;

public class IdempotencyKey
{
    public string CallerId { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public Guid JobId { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresUtc { get; set; }
}
