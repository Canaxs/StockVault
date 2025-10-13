using Core.Security.Entities;
using Core.Security.Hashing;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeders;

public  class DbInitializer
{
    public static void Seed(BaseDbContext context)
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
            context.SaveChanges();

            var allClaims = context.OperationClaims.ToList();
            var adminClaims = allClaims
                .Select(claim => new UserOperationClaim(adminUser.Id, claim.Id))
                .ToList();

            context.UserOperationClaims.AddRange(adminClaims);
            context.SaveChanges();
        }
    }
}
