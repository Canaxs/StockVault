using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Shipment: Entity<int>
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }

    public DeliveryStatus DeliveryStatus { get; set; }

    public string? Notes { get; set; }

    public virtual Product? Product { get; set; }
    public virtual Warehouse? Warehouse { get; set; }
    public virtual Customer? Customer { get; set; }

    // Burada collections initialize set etmememize rağmen parametresiz constructor kullandık çünkü ef core dbden veri çekerken ihtiyaç duyuyor
    public Shipment()
    {
        
    }

    // :this() kullanmadım çünkü Bir collection'ı parametresiz constructorda initialize etmedik 
    public Shipment(int productId, int warehouseId, int customerId, int quantity, DeliveryStatus deliveryStatus , string notes)
    {
        ProductId = productId;
        WarehouseId = warehouseId;
        CustomerId = customerId;
        Quantity = quantity;
        DeliveryStatus = deliveryStatus;
        Notes = notes;

    }
}

