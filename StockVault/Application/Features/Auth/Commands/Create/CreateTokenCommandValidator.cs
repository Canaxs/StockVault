using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Create;

public class CreateTokenCommandValidator:AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator()
    {
        RuleFor(u => u.Username).NotEmpty();
        RuleFor(u => u.Password).NotEmpty();
    }
}
