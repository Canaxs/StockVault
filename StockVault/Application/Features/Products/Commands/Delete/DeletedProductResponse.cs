using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Delete;

public class DeletedProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public DateTime DeletedDate { get; set; }
}
