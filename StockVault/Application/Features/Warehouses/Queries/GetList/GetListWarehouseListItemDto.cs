using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Queries.GetList;

public class GetListWarehouseListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentCapacity { get; set; }
}
