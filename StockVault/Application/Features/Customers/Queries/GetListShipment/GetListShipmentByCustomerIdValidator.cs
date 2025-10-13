using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetListShipment;

public class GetListShipmentByCustomerIdValidator:AbstractValidator<GetListShipmentByCustomerIdQuery>
{
    public GetListShipmentByCustomerIdValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
