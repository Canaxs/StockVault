using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Product : Entity<int>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }

    public virtual ICollection<ProductStock> ProductStocks { get; set;}
    public virtual ICollection<Shipment> Shipments { get; set; }

    public Product()
    {
        ProductStocks = new HashSet<ProductStock>();
    }

    public Product(string name, string description, double price):this()
    {
        Name = name;
        Description = description;
        Price = price;
    }
}
