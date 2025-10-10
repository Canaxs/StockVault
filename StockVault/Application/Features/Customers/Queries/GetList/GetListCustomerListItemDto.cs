using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetList;

public class GetListCustomerListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? CompanyName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
}
