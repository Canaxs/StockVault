using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListWarehouse;

public class GetListWarehouseByProductIdListItemDto
{
    public string WarehouseId { get; set; }
    public string WarehouseName { get; set; }
    public string WarehouseLocation { get; set; }
    public int Quantity { get; set; }
}
