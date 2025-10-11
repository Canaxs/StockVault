using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Commands.Update;

public class UpdateShipmentValidator:AbstractValidator<UpdateShipmentCommand>
{
    public UpdateShipmentValidator()
    {
        RuleFor(s => s.Id).NotNull();
        RuleFor(s => s.DeliveryStatus).NotEmpty();
    }
}
