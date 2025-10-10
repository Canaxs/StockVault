using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Commands.Create;

public class CreateProductStockValidator:AbstractValidator<CreateProductStockCommand>
{
    public CreateProductStockValidator()
    {
        RuleFor(ps => ps.ProductId).NotEmpty();
        RuleFor(ps => ps.WarehouseId).NotEmpty();
        RuleFor(ps => ps.Quantity).NotEmpty();
    }
}
