using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Commands.Update;

public class UpdatedWarehouseResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentCapacity { get; set; }
    public DateTime UpdatedDate { get; set; }
}
