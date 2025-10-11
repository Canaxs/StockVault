using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Queries.GetById;

public class GetByIdShipmentValidator:AbstractValidator<GetByIdShipmentQuery>
{
    public GetByIdShipmentValidator()
    {
        RuleFor(s => s.Id).NotNull();
    }
}
