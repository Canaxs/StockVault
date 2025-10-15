using Bogus;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders.Generators;

public static class ProductDataGenerator
{
    public static List<Product> Generate(int count)
    {
        var faker = new Faker<Product>("tr")
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => f.Random.Double(10, 2000));

        return faker.Generate(count);
    }
}
