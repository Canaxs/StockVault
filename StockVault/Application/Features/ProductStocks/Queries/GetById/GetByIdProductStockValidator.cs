using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Queries.GetById;

public class GetByIdProductStockValidator:AbstractValidator<GetByIdProductStockQuery>
{
    public GetByIdProductStockValidator()
    {
        RuleFor(ps => ps.Id).NotEmpty();
    }
}
