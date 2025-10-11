using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductStockConfiguration : IEntityTypeConfiguration<ProductStock>
{
    public void Configure(EntityTypeBuilder<ProductStock> builder)
    {
        builder.ToTable("ProductStocks").HasKey(p => p.Id);

        builder.Property(ps => ps.Id).HasColumnName("Id").IsRequired();
        builder.Property(ps => ps.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(ps => ps.WarehouseId).HasColumnName("WarehouseId").IsRequired();
        builder.Property(ps => ps.Quantity).HasColumnName("Quantity").IsRequired();

        builder.Property(ps => ps.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ps => ps.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ps => ps.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(ps => new { ps.ProductId, ps.WarehouseId }).IsUnique().HasFilter("[DeletedDate] IS NULL");

        builder.HasOne(ps => ps.Product);
        builder.HasOne(ps => ps.Warehouse);

        builder.HasQueryFilter(ps => !ps.DeletedDate.HasValue);
    }
}
