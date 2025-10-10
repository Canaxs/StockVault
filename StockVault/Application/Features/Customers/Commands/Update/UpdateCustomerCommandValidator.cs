using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().Length(3, 100);
        RuleFor(c => c.PhoneNumber).Length(13).Unless(c => string.IsNullOrEmpty(c.PhoneNumber));
    }
}
