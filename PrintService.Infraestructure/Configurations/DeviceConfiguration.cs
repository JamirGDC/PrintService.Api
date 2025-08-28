using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintService.Domain.Entities;

namespace PrintService.Infraestructure.Configurations;

public class DeviceConfiguration : EntityTypeBaseConfiguration<Device>
{
    protected override void ConfigurateTableName(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("Devices");
    }

    protected override void ConfigurateConstraints(EntityTypeBuilder<Device> builder)
    {
        builder.HasIndex(d => new { d.UserId, d.AgentRegion });
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(d => d.DeviceId);

        builder.Property(d => d.DeviceId).HasMaxLength(128).IsRequired();
        builder.Property(d => d.UserId).HasMaxLength(128);
        builder.Property(d => d.AgentRegion).HasMaxLength(32).IsRequired();
        builder.Property(d => d.MachineName).HasMaxLength(256).IsRequired();
    }
}
