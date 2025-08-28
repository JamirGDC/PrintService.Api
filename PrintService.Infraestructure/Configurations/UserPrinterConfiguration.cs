using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintService.Domain.Entities;

namespace PrintService.Infraestructure.Configurations;

public class UserPrinterConfiguration : EntityTypeConfiguration<UserPrinter>
{
    protected override void ConfigurateTableName(EntityTypeBuilder<UserPrinter> builder)
    {
        builder.ToTable("UserPrinters");
    }

    protected override void ConfigurateConstraints(EntityTypeBuilder<UserPrinter> builder)
    {
        builder.HasKey(up => new { up.UserId, up.PrinterKey });
        builder.HasIndex(up => new { up.UserId, up.IsDefault });
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<UserPrinter> builder)
    {
        builder.Property(p => p.UserId).HasMaxLength(128).IsRequired();
        builder.Property(p => p.PrinterKey).HasMaxLength(256).IsRequired();
        builder.Property(p => p.IsDefault).HasDefaultValue(true);
        builder.Property(p => p.ChangedBy).HasMaxLength(128).IsRequired();
    }
}
