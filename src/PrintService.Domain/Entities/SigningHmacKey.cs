using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService.Domain.Entities;

public class SigningHmacKey
{
    public string OwnerCode { get; set; } = string.Empty;
    public int KeyId { get; set; }

    public byte[] Secret { get; set; } = Array.Empty<byte>();

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresUtc { get; set; }
    public bool IsActive { get; set; } = true;
}
