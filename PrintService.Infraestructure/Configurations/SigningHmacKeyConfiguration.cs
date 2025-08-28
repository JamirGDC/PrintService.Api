using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintService.Domain.Entities;

namespace PrintService.Infraestructure.Configurations;

public class SigningHmacKeyConfiguration : EntityTypeConfiguration<SigningHmacKey>
{
    protected override void ConfigurateTableName(EntityTypeBuilder<SigningHmacKey> builder)
    {
        builder.ToTable("SigningHmacKeys");
    }

    protected override void ConfigurateConstraints(EntityTypeBuilder<SigningHmacKey> builder)
    {
        builder.HasKey(k => new { k.OwnerCode, k.KeyId });
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<SigningHmacKey> builder)
    {
        builder.Property(p => p.OwnerCode).HasMaxLength(32).IsRequired();
        builder.Property(p => p.Secret).HasColumnType("varbinary(64)").IsRequired();
        builder.Property(p => p.IsActive).HasDefaultValue(true);
    }
}