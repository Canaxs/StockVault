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

    public ProductBusinessRules(IProductRepository productRepository)
    {
        _productRepository = productRepository;
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
            throw new Exception(ProductsMessages.ProductNameExists);
    }
}
