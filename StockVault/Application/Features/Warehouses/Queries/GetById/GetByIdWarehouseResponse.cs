using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Queries.GetById;

public class GetByIdWarehouseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentCapacity { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
