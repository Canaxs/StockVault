using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListTopSellingProduct;

public class GetListTopSellingProductListItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public int TotalQuantity { get; set; }
    public double TotalPrice { get; set; }

}
