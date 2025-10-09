using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class ProductStock : Entity<int>
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }

    public virtual Warehouse? Warehouse { get; set; }
    public virtual Product? Product { get; set; }

    public ProductStock()
    {
        
    }

    public ProductStock(int productId, int warehouseId, int quantity):this()
    {
        WarehouseId = warehouseId;
        Quantity = quantity;
        ProductId = productId;
    }
}
