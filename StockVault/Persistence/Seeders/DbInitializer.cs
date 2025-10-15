using Core.Security.Entities;
using Core.Security.Hashing;
using Persistence.Contexts;
using Persistence.Seeders.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders;

public static class DbInitializer
{
    public static async Task Seed(BaseDbContext context)
    {
        if (!context.Users.Any())
        {
            HashingHelper.CreatePasswordHash(
                    password: "Passw0rd",
                    passwordHash: out byte[] passwordHash,
                    passwordSalt: out byte[] passwordSalt
                );

            var adminUser = new User
            {
                Username = "Admin",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.UtcNow
            };

            context.Users.Add(adminUser);
            await context.SaveChangesAsync();

            var allClaims = context.OperationClaims.ToList();
            var adminClaims = allClaims
                .Select(claim => new UserOperationClaim(adminUser.Id, claim.Id))
                .ToList();

            context.UserOperationClaims.AddRange(adminClaims);
            await context.SaveChangesAsync();
        }
    

        if(!context.Warehouses.Any() && !context.Products.Any() && !context.Customers.Any())
        {
            var warehouses = WarehouseDataGenerator.Generate(5);
            context.Warehouses.AddRange(warehouses);
            await context.SaveChangesAsync();

            var products = ProductDataGenerator.Generate(50);
            context.Products.AddRange(products);
            await context.SaveChangesAsync();

            var customers = CustomerDataGenerator.Generate(20);
            context.Customers.AddRange(customers);
            await context.SaveChangesAsync();

            var stocks = ProductStockDataGenerator.Generate(products, warehouses);
            context.ProductStocks.AddRange(stocks);
            await context.SaveChangesAsync();
        }
    
    }
}
