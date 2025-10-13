using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Queries.GetProductStockByProductAndWarehouse;

public class GetProductStockByProductIdAndWarehouseIdValidator:AbstractValidator<GetProductStockByProductIdAndWarehouseIdQuery>
{
    public GetProductStockByProductIdAndWarehouseIdValidator()
    {
        RuleFor(ps => ps.ProductId).NotEmpty();
        RuleFor(ps => ps.WarehouseId).NotEmpty();
    }
}
