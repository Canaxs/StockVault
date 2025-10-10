using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.Create;

public class CreateCustomerCommandValidator:AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().Length(3, 100);
        RuleFor(c => c.PhoneNumber).Length(13);
    }
}
