using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListShipmentSummary;

public class GetShipmentSummaryByProductIdValidator:AbstractValidator<GetShipmentSummaryByProductIdQuery>
{
    public GetShipmentSummaryByProductIdValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}
