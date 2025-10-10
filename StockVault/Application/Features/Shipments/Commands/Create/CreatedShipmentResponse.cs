using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Commands.Create;

public class CreatedShipmentResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public string? Notes { get; set; }

}
