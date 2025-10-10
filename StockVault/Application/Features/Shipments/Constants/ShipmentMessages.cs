using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Constants;

public class ShipmentMessages
{
    public const string ProductNotFoundInWarehouse = "The specified product was not found in the selected warehouse.";
    public const string InsufficientProductStock = "Insufficient stock quantity for the requested shipment.";
    public const string CustomerNotFound = "Customer not found.";
}
