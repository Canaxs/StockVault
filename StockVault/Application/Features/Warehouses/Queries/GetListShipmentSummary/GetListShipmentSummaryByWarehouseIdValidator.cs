using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Queries.GetListShipmentSummary;

public class GetListShipmentSummaryByWarehouseIdValidator:AbstractValidator<GetListShipmentSummaryByWarehouseIdQuery>
{
    public GetListShipmentSummaryByWarehouseIdValidator()
    {
        RuleFor(w => w.Id).NotEmpty();
    }
}
