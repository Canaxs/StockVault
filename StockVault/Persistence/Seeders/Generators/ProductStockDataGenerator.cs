using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders.Generators;

public static class ProductStockDataGenerator
{
    public static List<ProductStock> Generate(List<Product> products, List<Warehouse> warehouses)
    {
        var random = Random.Shared;
        var stocks = new List<ProductStock>();

        int maxCount = Math.Min(products.Count, 15);
        int minCount = Math.Min(products.Count, 5);

        foreach (var warehouse in warehouses)
        {
            if (warehouse.CurrentCapacity >= warehouse.MaxCapacity)
                continue;

            var productsInWarehouse = products.OrderBy(_ => random.Next()).Take(random.Next(minCount, maxCount+1)).ToList();

            foreach (var product in productsInWarehouse)
            {

                int remainingCapacity = warehouse.MaxCapacity - warehouse.CurrentCapacity;
                if (remainingCapacity <= 0)
                    break;


                int quantity = random.Next(10, 200);

                if (quantity > remainingCapacity)
                    quantity = remainingCapacity;

                stocks.Add(new ProductStock
                {
                    ProductId = product.Id,
                    WarehouseId = warehouse.Id,
                    Quantity = quantity
                });

                warehouse.CurrentCapacity += quantity;
            }
        }

        return stocks;
    }
}
