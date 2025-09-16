using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService.Domain.Entities;

public class OutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Region { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;

    public DateTime OccurredUtc { get; set; } = DateTime.UtcNow;
    public int Attempts { get; set; } = 0;
    public DateTime NextVisibleUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedUtc { get; set; }
    public string? Error { get; set; }
}