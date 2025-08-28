using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintService.Domain.Entities;

namespace PrintService.Infraestructure.Configurations;

public class IdempotencyKeyConfiguration : EntityTypeConfiguration<IdempotencyKey>
{
    protected override void ConfigurateTableName(EntityTypeBuilder<IdempotencyKey> builder)
    {
        builder.ToTable("IdempotencyKeys");
    }

    protected override void ConfigurateConstraints(EntityTypeBuilder<IdempotencyKey> builder)
    {
        builder.HasKey(i => new { i.CallerId, i.Key });
        builder.HasOne<PrintJob>()
            .WithMany()
            .HasForeignKey(i => i.JobId);
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<IdempotencyKey> builder)
    {
        builder.Property(p => p.CallerId).HasMaxLength(128).IsRequired();
        builder.Property(p => p.Key).HasMaxLength(64).IsRequired();
    }
}
