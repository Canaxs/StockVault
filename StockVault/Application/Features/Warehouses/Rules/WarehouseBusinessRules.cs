using Application.Features.Warehouses.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
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

    public WarehouseBusinessRules(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    public async Task WarehouseNameCannotBeDuplicatedWhenInserted(string name)
    {
        bool result = await _warehouseRepository.AnyAsync(predicate: w => w.Name.ToLower() == name.ToLower());

        if (result)
        {
            throw new Exception(WarehousesMessages.WarehouseNameExists);
        }
    }

    public async Task WarehouseShouldExistWhenRequested(int id)
    {
        bool result = await _warehouseRepository.AnyAsync(predicate: w => w.Id == id);

        if (!result)
            throw new Exception(WarehousesMessages.WarehouseNotFoundOrAlreadyDeleted);
    }

    public void CheckIfMaxCapacityIsValid(int newMaxCapacity , Warehouse warehouse)
    {
        if (newMaxCapacity < warehouse.CurrentCapacity)
            throw new Exception(WarehousesMessages.MaxCapacityCannotBeLessThanCurrentCapacity);
    }
}
