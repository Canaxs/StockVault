using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments").HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();

        builder.Property(s => s.ProductId).HasColumnName("ProductId").IsRequired();

        builder.Property(s => s.WarehouseId).HasColumnName("WarehouseId").IsRequired();

        builder.Property(s => s.CustomerId).HasColumnName("CustomerId").IsRequired();

        builder.Property(s => s.Quantity).HasColumnName("Quantity").IsRequired();

        builder.Property(s => s.DeliveryStatus).HasColumnName("Status").IsRequired();

        builder.Property(s => s.Notes).HasColumnName("Notes");

        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(s => s.Product);
        builder.HasOne(s => s.Warehouse);
        builder.HasOne(s => s.Customer);

        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }

}
