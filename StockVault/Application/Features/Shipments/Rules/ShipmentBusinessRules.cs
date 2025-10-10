using Application.Features.ProductStocks.Constants;
using Application.Features.Shipments.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
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

    public ShipmentBusinessRules(IProductStockRepository productStockRepository, IShipmentRepository shipmentRepository, ICustomerRepository customerRepository)
    {
        _productStockRepository = productStockRepository;
        _shipmentRepository = shipmentRepository;
        _customerRepository = customerRepository;
    }

    public async Task<ProductStock> CheckIfSufficientProductStockExists(int productId, int warehouseId, int quantity)
    {
        ProductStock? result = await _productStockRepository.GetAsync(
            predicate: ps => ps.ProductId == productId && ps.WarehouseId == warehouseId
            );

        if (result is null)
            throw new Exception(ShipmentMessages.ProductNotFoundInWarehouse);

        if (result.Quantity < quantity)
            throw new Exception(ShipmentMessages.InsufficientProductStock);

        return result;
    }

    public async Task CheckIfCustomerIdExists(int customerId)
    {
        bool result = await _customerRepository.AnyAsync(c => c.Id == customerId);

        if (!result)
            throw new Exception(ShipmentMessages.CustomerNotFound);
    }

}
