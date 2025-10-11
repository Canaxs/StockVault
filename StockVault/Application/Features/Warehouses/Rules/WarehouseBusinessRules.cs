using Application.Features.Products.Constants;
using Application.Features.Warehouses.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Rules;

public class WarehouseBusinessRules:BaseBusinessRules
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProductStockRepository _productStockRepository;

    public WarehouseBusinessRules(IWarehouseRepository warehouseRepository, IProductStockRepository productStockRepository)
    {
        _warehouseRepository = warehouseRepository;
        _productStockRepository = productStockRepository;
    }
    public async Task WarehouseNameCannotBeDuplicatedWhenInserted(string name)
    {
        bool result = await _warehouseRepository.AnyAsync(predicate: w => w.Name.ToLower() == name.ToLower());

        if (result)
            throw new BusinessException(WarehousesMessages.WarehouseNameExists);
    }

    public async Task WarehouseNameCannotBeDuplicatedWhenUpdated(string name,int id)
    {
        bool result = await _warehouseRepository.AnyAsync(predicate: w => w.Name.ToLower() == name.ToLower() && w.Id != id);

        if (result)
            throw new BusinessException(WarehousesMessages.WarehouseNameExists);
    }

    public async Task WarehouseShouldExistWhenRequested(int id)
    {
        bool result = await _warehouseRepository.AnyAsync(predicate: w => w.Id == id);

        if (!result)
            throw new NotFoundException(WarehousesMessages.WarehouseNotFoundOrAlreadyDeleted);
    }

    public void CheckIfMaxCapacityIsValid(int newMaxCapacity , Warehouse warehouse)
    {
        if (newMaxCapacity < warehouse.CurrentCapacity)
            throw new BusinessException(WarehousesMessages.MaxCapacityCannotBeLessThanCurrentCapacity);
    }

    public async Task CheckIfWarehouseHasNoStockBeforeDeletion(int warehouseId)
    {
        bool hasStock = await _productStockRepository.AnyAsync(
            ps => ps.WarehouseId == warehouseId && ps.Quantity > 0
        );

        if (hasStock)
            throw new BusinessException(WarehousesMessages.WarehouseNotEmptyCannotBeDeleted);

    }

}
