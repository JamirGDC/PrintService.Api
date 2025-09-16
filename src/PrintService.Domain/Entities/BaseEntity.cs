using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintService.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedUtc { get; set; }

    public DateTime? ExpiresUtc { get; set; }

    [Timestamp]
    public byte[]? RowVersion { get; set; }
}
