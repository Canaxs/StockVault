using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetListProduct;

public class GetListProductByCustomerIdValidator:AbstractValidator<GetListProductByCustomerIdQuery>
{
    public GetListProductByCustomerIdValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
