using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Commands.Delete;

public class DeleteShipmentValidator:AbstractValidator<DeleteShipmentCommand>
{
    public DeleteShipmentValidator()
    {
        RuleFor(s => s.Id).NotNull();
    }
}
