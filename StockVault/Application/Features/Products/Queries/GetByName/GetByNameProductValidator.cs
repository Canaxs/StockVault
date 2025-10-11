using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetByName;

public class GetByNameProductValidator:AbstractValidator<GetByNameProductQuery>
{
    public GetByNameProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}
