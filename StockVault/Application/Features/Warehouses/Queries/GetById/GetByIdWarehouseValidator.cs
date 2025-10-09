using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Queries.GetById;

public class GetByIdWarehouseValidator:AbstractValidator<GetByIdWarehouseQuery>
{
    public GetByIdWarehouseValidator()
    {
        RuleFor(w => w.Id).NotEmpty();
    }
    
}
