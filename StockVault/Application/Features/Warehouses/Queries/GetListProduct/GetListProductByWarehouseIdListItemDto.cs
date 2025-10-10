using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Queries.GetListProduct;

public class GetListProductByWarehouseIdListItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public double ProductPrice { get; set; }
    public int Quantity { get; set; }

    /*public GetListProductByWarehouseIdListItemDto(int id,string name,string description,double price ,int quantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
    }
    */
}
