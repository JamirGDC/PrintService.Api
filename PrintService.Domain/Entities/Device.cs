using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService.Domain.Entities;

public class Device : BaseEntity
{
    public string? UserId { get; set; }
    public string AgentRegion { get; set; } = string.Empty;
    public string MachineName { get; set; } = string.Empty;
    public string? PrintersJson { get; set; }
    public DateTime LastSeenUtc { get; set; } = DateTime.UtcNow;
}
