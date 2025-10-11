using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Queries.GetListShipment;

public class GetListShipmentByWarehouseIdValidator:AbstractValidator<GetListShipmentByWarehouseIdQuery>
{
    public GetListShipmentByWarehouseIdValidator()
    {
        RuleFor(w => w.Id).NotEmpty();
    }
}
