using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListShipmentSummary;

public class GetShipmentSummaryByProductIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int TotalQuantity { get; set; }
    public double TotalPrice { get; set; }
}
