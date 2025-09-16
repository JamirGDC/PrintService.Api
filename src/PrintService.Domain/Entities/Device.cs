using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService.Domain.Entities;

public class Device
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedUtc { get; set; }

    public DateTime? ExpiresUtc { get; set; }

    [Timestamp]
    public byte[]? RowVersion { get; set; }
    public string? UserId { get; set; }
    public string AgentRegion { get; set; } = string.Empty;
    public string MachineName { get; set; } = string.Empty;
    public string? PrintersJson { get; set; }
    public DateTime LastSeenUtc { get; set; } = DateTime.UtcNow;
}
