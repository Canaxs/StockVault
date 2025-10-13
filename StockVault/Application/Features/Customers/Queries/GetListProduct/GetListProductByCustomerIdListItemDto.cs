using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetListProduct;

public class GetListProductByCustomerIdListItemDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public double ProductPrice { get; set; }
    public int TotalQuantity { get; set; }
    public double TotalPrice { get; set; }
}
