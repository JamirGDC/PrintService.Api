using System.ComponentModel.DataAnnotations;

namespace PrintService.Domain.Entities;

public class PrintJob
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedUtc { get; set; }

    public DateTime? ExpiresUtc { get; set; }

    [Timestamp]
    public byte[]? RowVersion { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string? DeviceId { get; set; }
    public string Region { get; set; } = string.Empty;
    public string PrinterKey { get; set; } = string.Empty;
    public string? ActualPrinterName { get; set; }
    public string ContentType { get; set; } = "zpl";
    public string? Payload { get; set; }
    public byte[]? PayloadHash { get; set; }
    public int SignatureKeyId { get; set; }
    public byte[]? Signature { get; set; }
    public byte Status { get; set; } = 0;
    public int Attempts { get; set; } = 0;
    public DateTime? ClaimedUtc { get; set; }
    public DateTime? CompletedUtc { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}
