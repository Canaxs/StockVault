using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListByDynamicName;

public class GetListByDynamicNameValidator : AbstractValidator<GetListByDynamicNameQuery>
{
    public GetListByDynamicNameValidator()
    {
        RuleFor(d => d.FieldValue).NotEmpty();
        RuleFor(d => d.FieldOperator).NotEmpty();
        RuleFor(d => d.SortField).NotEmpty();
        RuleFor(d => d.SortDir).NotEmpty();
    }
}
