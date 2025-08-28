using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintService.Domain.Entities;

namespace PrintService.Infraestructure.Configurations;

public class OutboxMessageConfiguration : EntityTypeConfiguration<OutboxMessage>
{
    protected override void ConfigurateTableName(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
    }

    protected override void ConfigurateConstraints(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasIndex(o => new { o.ProcessedUtc, o.NextVisibleUtc, o.OccurredUtc });
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Region).HasMaxLength(64).IsRequired();
        builder.Property(o => o.Type).HasMaxLength(200).IsRequired();
        builder.Property(o => o.Payload).HasMaxLength(200).IsRequired();
    }
}
