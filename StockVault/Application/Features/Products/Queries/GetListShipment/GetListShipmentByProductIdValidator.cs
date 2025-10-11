using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListShipment;

public class GetListShipmentByProductIdValidator:AbstractValidator<GetListShipmentByProductIdQuery>
{
    public GetListShipmentByProductIdValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}
