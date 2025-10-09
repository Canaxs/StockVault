using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Commands.Create;

public class CreateWarehouseCommandValidator:AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {
        RuleFor(w => w.Name).NotEmpty().MinimumLength(3);
        RuleFor(w => w.Location).NotEmpty().MinimumLength(10);
        RuleFor(w => w.MaxCapacity).NotEmpty();
    }
}
