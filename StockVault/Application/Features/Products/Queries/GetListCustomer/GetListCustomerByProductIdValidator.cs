using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListCustomer;

public class GetListCustomerByProductIdValidator:AbstractValidator<GetListCustomerByProductIdQuery>
{
    public GetListCustomerByProductIdValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}
