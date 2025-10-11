using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Queries.GetListShipment;

public class GetListShipmentByWarehouseIdListItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; }
    public string CustomerName { get; set; }
    public int Quantity { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public DateTime CreatedDate { get; set; }
}
