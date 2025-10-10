using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Commands.Update;

public class UpdateProductStockValidator:AbstractValidator<UpdateProductStockCommand>
{
    public UpdateProductStockValidator()
    {
        RuleFor(ps => ps.Id).NotEmpty();
        RuleFor(ps => ps.Quantity).NotEmpty();
    }
}
