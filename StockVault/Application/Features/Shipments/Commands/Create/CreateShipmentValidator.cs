using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Commands.Create;

public class CreateShipmentValidator:AbstractValidator<CreateShipmentCommand>
{
    public CreateShipmentValidator()
    {
        RuleFor(s => s.ProductId).NotEmpty();
        RuleFor(s => s.WarehouseId).NotEmpty();
        RuleFor(s => s.CustomerId).NotEmpty();
        RuleFor(s => s.Quantity).NotNull().GreaterThan(0);
    }
}
