using Core.Application.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListTopSellingProduct;

public class GetListTopSellingProductValidator:AbstractValidator<GetListTopSellingProductQuery>
{
    public GetListTopSellingProductValidator()
    {
       
    }
}
