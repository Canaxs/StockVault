using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.Delete;

public class DeleteCustomerValidator:AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
