using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService.Domain.Entities;

public class UserPrinter
{
    public string UserId { get; set; } = string.Empty;
    public string PrinterKey { get; set; } = string.Empty;

    public bool IsDefault { get; set; } = true;
    public DateTime ChangedUtc { get; set; } = DateTime.UtcNow;
    public string ChangedBy { get; set; } = string.Empty;
}