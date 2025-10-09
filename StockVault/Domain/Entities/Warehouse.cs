using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Warehouse:Entity<int>
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentCapacity { get; set; }

    public Warehouse()
    {
        
    }

    public Warehouse(string name, string location, int maxCapacity, int currentCapacity)
    {
        Name = name;
        Location = location;
        MaxCapacity = maxCapacity;
        CurrentCapacity = currentCapacity;
    }
}
