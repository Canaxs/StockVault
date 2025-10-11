using Application.Features.Products.Constants;
using Application.Features.Warehouses.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
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
            throw new BusinessException(ProductsMessages.ProductNameExists);
    }

    public async Task ProductNameCannotBeDuplicatedWhenUpdated(string name, int id)
    {
        bool result = await _productRepository.AnyAsync(predicate: p => p.Name.ToLower() == name.ToLower() && p.Id != id);

        if (result)
            throw new BusinessException(ProductsMessages.ProductNameExists);
    }

    public async Task ProductShouldExistWhenRequested(int id)
    {
        bool result = await _productRepository.AnyAsync(predicate: p => p.Id == id);

        if (!result)
            throw new NotFoundException(ProductsMessages.ProductNotFoundOrAlreadyDeleted);
    }

    public async Task CheckIfProductHasStockBeforeDeletionAsync(int id)
    {
        bool hasStock = await _productStockRepository.AnyAsync(
            ps => ps.ProductId == id && ps.Quantity > 0
        );

        if (hasStock)
            throw new BusinessException(ProductsMessages.ProductHasStockCannotBeDeleted);
    }
    
    public async Task CheckProductNameExists(string name)
    {
        bool result = await _productRepository.AnyAsync(predicate: p => p.Name.ToLower() == name.ToLower());

        if (!result)
            throw new NotFoundException(ProductsMessages.ProductNameNotFound);

    }
}
