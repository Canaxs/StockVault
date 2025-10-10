using Application.Features.ProductStocks.Constants;
using Application.Features.Warehouses.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Rules;

public class ProductStockBusinessRules:BaseBusinessRules
{
    private readonly IProductStockRepository _productStockRepository;
    private readonly IWarehouseRepository _warehouseRepository;

    public ProductStockBusinessRules(IProductStockRepository productStockRepository, IWarehouseRepository warehouseRepository)
    {
        _productStockRepository = productStockRepository;
        _warehouseRepository = warehouseRepository;
    }

    public async Task CheckProductWarehouseUniqueness(int productId, int warehouseId)
    {
        bool result = await _productStockRepository.AnyAsync(ps => ps.WarehouseId == warehouseId && ps.ProductId == productId);

        if (result)
            throw new Exception(ProduckStockMessages.ProductAlreadyInWarehouse);
    }

    public async Task CheckIfProductStockIdExists(int id)
    {
        bool result = await _productStockRepository.AnyAsync(ps => ps.Id == id);

        if (!result)
            throw new Exception(ProduckStockMessages.ProductStockNotExist);
    }

    public async Task CheckWarehouseHasEnoughCapacity(int warehouseId, int quantity)
    {
        Warehouse warehouse = await _warehouseRepository.GetAsync(w => w.Id == warehouseId);

        if (warehouse.CurrentCapacity + quantity > warehouse.MaxCapacity)
            throw new Exception(ProduckStockMessages.NotEnoughSpaceForStock);
    }
}
