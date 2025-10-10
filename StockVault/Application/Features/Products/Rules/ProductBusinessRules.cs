using Application.Features.Products.Constants;
using Application.Features.Warehouses.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Rules;

public class ProductBusinessRules:BaseBusinessRules
{
    private readonly IProductRepository _productRepository;
    private readonly IProductStockRepository _productStockRepository;

    public ProductBusinessRules(IProductRepository productRepository, IProductStockRepository productStockRepository)
    {
        _productRepository = productRepository;
        _productStockRepository = productStockRepository;
    }

    public async Task ProductNameCannotBeDuplicatedWhenInserted(string name)
    {
        bool result = await _productRepository.AnyAsync(predicate: p => p.Name.ToLower() == name.ToLower());

        if (result)
        {
            throw new Exception(ProductsMessages.ProductNameExists);
        }
    }
    public async Task ProductShouldExistWhenRequested(int id)
    {
        bool result = await _productRepository.AnyAsync(predicate: p => p.Id == id);

        if (!result)
            throw new Exception(ProductsMessages.ProductNotFoundOrAlreadyDeleted);
    }

    public async Task CheckIfProductHasStockBeforeDeletionAsync(int id)
    {
        var productStocks = await _productStockRepository.GetListProjectedAsync(
            predicate: ps => ps.ProductId == id,
            selector: ps => ps.Quantity
            );

        int totalQuantity = productStocks.Items.Sum();

        if (totalQuantity > 0)
            throw new Exception(ProductsMessages.ProductHasStockCannotBeDeleted);
    }
}
