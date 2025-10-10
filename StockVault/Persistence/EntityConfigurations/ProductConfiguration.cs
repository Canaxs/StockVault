using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
        builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(250);
        builder.Property(p => p.Price).HasColumnName("Price").IsRequired();
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: p => p.Name, name: "UK_Products_Name").IsUnique();

        builder.HasMany(p => p.ProductStocks);
        builder.HasMany(p => p.Shipments);

        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}
