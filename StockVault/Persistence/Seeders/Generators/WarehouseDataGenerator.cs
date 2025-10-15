using Bogus;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders.Generators;

public static class WarehouseDataGenerator
{
    public static List<Warehouse> Generate(int count)
    {
        var faker = new Faker<Warehouse>("tr")
            .RuleFor(w => w.Name, f => $"{f.Company.CompanyName()} Deposu")
            .RuleFor(w => w.Location, f => f.Address.City())
            .RuleFor(w => w.MaxCapacity, f => f.Random.Int(500, 2000))
            .RuleFor(w => w.CurrentCapacity, 0);

        return faker.Generate(count);
    }
}
