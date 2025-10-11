using Application.Features.ProductStocks.Constants;
using Application.Features.Shipments.Constants;
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

namespace Application.Features.Shipments.Rules;

public class ShipmentBusinessRules:BaseBusinessRules
{
    private readonly IProductStockRepository _productStockRepository;
    private readonly IShipmentRepository _shipmentRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IWarehouseRepository _warehouseRepository;

    public ShipmentBusinessRules(IProductStockRepository productStockRepository, IShipmentRepository shipmentRepository, ICustomerRepository customerRepository, IWarehouseRepository warehouseRepository)
    {
        _productStockRepository = productStockRepository;
        _shipmentRepository = shipmentRepository;
        _customerRepository = customerRepository;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<ProductStock> CheckIfSufficientProductStockExists(int productId, int warehouseId, int quantity)
    {
        ProductStock? result = await _productStockRepository.GetAsync(
            predicate: ps => ps.ProductId == productId && ps.WarehouseId == warehouseId
            );

        if (result is null)
            throw new NotFoundException(ShipmentMessages.ProductNotFoundInWarehouse);

        if (result.Quantity < quantity)
            throw new BusinessException(ShipmentMessages.InsufficientProductStock);

        return result;
    }

    public async Task CheckIfProductStockExists(int productId, int warehouseId)
    {
        bool result = await _productStockRepository.AnyAsync(predicate: ps => ps.ProductId == productId && ps.WarehouseId == warehouseId);

        if (!result)
            throw new NotFoundException(ShipmentMessages.ProductStockNotFound);
    }

    public async Task CheckIfWarehouseHasEnoughCapacity(int warehouseId,int quantity)
    {
        Warehouse? warehouse = await _warehouseRepository.GetAsync(w => w.Id == warehouseId);

        if (warehouse.CurrentCapacity + quantity > warehouse.MaxCapacity)
            throw new BusinessException(ShipmentMessages.InsufficientProductStock);
    }


    public async Task CheckIfCustomerIdExists(int customerId)
    {
        bool result = await _customerRepository.AnyAsync(c => c.Id == customerId);

        if (!result)
            throw new NotFoundException(ShipmentMessages.CustomerNotFound);
    }

    public async Task CheckIfShipmentIdExists(int id)
    {
        bool result = await _shipmentRepository.AnyAsync(c => c.Id == id);

        if (!result)
            throw new NotFoundException(ShipmentMessages.ShipmentNotFound);
    }

    public async Task CheckIfShipmentExistsAndIsPending(int id)
    {
        bool result = await _shipmentRepository.AnyAsync(c => c.Id == id && c.DeliveryStatus == Domain.Enums.DeliveryStatus.Pending);

        if (!result)
            throw new BusinessException(ShipmentMessages.ShipmentNotFoundOrNotPending);
    }

}
