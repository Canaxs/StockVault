using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext, IDbContext
{
    protected IConfiguration? Configuration { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductStock> ProductStock { get; set; }
    public DbSet<Shipment> Shipment { get; set; }
    public DbSet<Customer> Customer { get; set; }


    public BaseDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
