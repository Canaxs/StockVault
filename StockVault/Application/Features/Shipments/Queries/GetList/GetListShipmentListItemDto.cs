using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Queries.GetList;

public class GetListShipmentListItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
}
