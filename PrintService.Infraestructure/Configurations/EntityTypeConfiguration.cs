using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintService.Domain.Entities;

namespace PrintService.Infraestructure.Configurations;

public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        ConfigurateProperties(builder);
        ConfigurateConstraints(builder);
        ConfigurateTableName(builder);
        SeedData(builder);
    }
    protected abstract void ConfigurateProperties(EntityTypeBuilder<T> builder);
    protected abstract void ConfigurateConstraints(EntityTypeBuilder<T> builder);
    protected abstract void ConfigurateTableName(EntityTypeBuilder<T> builder);
    protected virtual void SeedData(EntityTypeBuilder<T> builder) { }
}