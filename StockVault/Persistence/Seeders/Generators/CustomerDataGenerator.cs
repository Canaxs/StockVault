using Bogus;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders.Generators;

public static class CustomerDataGenerator
{
    public static List<Customer> Generate(int count)
    {
        var faker = new Faker<Customer>("tr")
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
            .RuleFor(c => c.Address, f => f.Address.StreetAddress())
            .RuleFor(c => c.City, f => f.Address.City())
            .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("### ### ## ##"));

        return faker.Generate(count);
    }
}
