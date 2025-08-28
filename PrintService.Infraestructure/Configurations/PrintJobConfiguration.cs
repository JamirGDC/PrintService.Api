using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintService.Domain.Entities;

namespace PrintService.Infraestructure.Configurations;

public class PrintJobConfiguration : EntityTypeBaseConfiguration<PrintJob>
{
    protected override void ConfigurateTableName(EntityTypeBuilder<PrintJob> builder)
    {
        builder.ToTable("PrintJobs");
    }

    protected override void ConfigurateConstraints(EntityTypeBuilder<PrintJob> builder)
    {
        builder.HasIndex(p => new { p.Status, p.CreatedUtc });
        builder.HasIndex(p => new { p.UserId, p.Status });
        builder.HasIndex(p => new { p.Region, p.Status });
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<PrintJob> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.UserId).HasMaxLength(128).IsRequired();
        builder.Property(p => p.DeviceId).HasMaxLength(128);
        builder.Property(p => p.Region).HasMaxLength(32).IsRequired();
        builder.Property(p => p.PrinterKey).HasMaxLength(256).IsRequired();
        builder.Property(p => p.ActualPrinterName).HasMaxLength(256);
        builder.Property(p => p.ContentType).HasMaxLength(16).IsRequired();
        builder.Property(p => p.PayloadHash).HasColumnType("varbinary(32)");
        builder.Property(p => p.Signature).HasColumnType("varbinary(64)");
        builder.Property(p => p.Status).HasColumnType("tinyint").IsRequired();
        builder.Property(p => p.ErrorCode).HasMaxLength(64);
        builder.Property(p => p.ErrorMessage).HasMaxLength(1024);

        builder.Property(p => p.RowVersion).IsRowVersion();
    }

    protected override void SeedData(EntityTypeBuilder<PrintJob> builder)
    {
        
    }
}
