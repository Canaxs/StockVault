using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Customer : Entity<int>
{
    public string Name { get; set; }
    public string? CompanyName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }

    public string PhoneNumber { get; set; }

    public virtual ICollection<Shipment> Shipments { get; set; }

    public Customer()
    {
        Shipments = new HashSet<Shipment>();
    }

    public Customer(string name, string companyName, string address, string city,string phoneNumber):this()
    {
        Name = name;
        CompanyName = companyName;
        Address = address;
        City = city;
        PhoneNumber = phoneNumber;
    }
}
