using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListWarehouse;

public class GetListWarehouseByProductIdValidator:AbstractValidator<GetListWarehouseByProductIdQuery>
{
    public GetListWarehouseByProductIdValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}
