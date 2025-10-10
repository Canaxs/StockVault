using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses").HasKey(w => w.Id);

        builder.Property(w => w.Id).HasColumnName("Id").IsRequired();

        builder.Property(w => w.Name).HasColumnName("Name").IsRequired().HasMaxLength(200);

        builder.Property(w => w.Location).HasColumnName("Location").IsRequired();

        builder.Property(w => w.MaxCapacity).HasColumnName("MaxCapacity").IsRequired();

        builder.Property(w => w.CurrentCapacity).HasColumnName("CurrentCapacity").IsRequired().HasDefaultValue(0);

        builder.Property(w => w.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(w => w.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(w => w.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: w => w.Name, name: "UK_Warehouses_Name").IsUnique();

        builder.HasMany(w => w.ProductStocks);
        builder.HasMany(w => w.Shipments);

        builder.HasQueryFilter(w => !w.DeletedDate.HasValue);
    }
}
