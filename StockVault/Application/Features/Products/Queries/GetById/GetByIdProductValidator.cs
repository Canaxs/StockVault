using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetById;

public class GetByIdProductValidator:AbstractValidator<GetByIdProductQuery>
{
    public GetByIdProductValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}
