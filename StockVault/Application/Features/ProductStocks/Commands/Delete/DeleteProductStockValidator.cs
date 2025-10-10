using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Commands.Delete;

public class DeleteProductStockValidator:AbstractValidator<DeleteProductStockCommand>
{
    public DeleteProductStockValidator()
    {
        RuleFor(ps => ps.Id).NotEmpty();
    }
}
