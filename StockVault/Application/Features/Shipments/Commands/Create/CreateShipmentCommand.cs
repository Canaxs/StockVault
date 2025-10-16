using Application.Features.Shipments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Commands.Create;

public class CreateShipmentCommand:IRequest<CreatedShipmentResponse>, ITransactionalRequest, ILoggableRequest
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
    public string? Notes { get; set; }

    public class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, CreatedShipmentResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IProductStockRepository _productStockRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly ShipmentBusinessRules _shipmentBusinessRules;
        private readonly IMapper _mapper;

        public CreateShipmentCommandHandler(IShipmentRepository shipmentRepository, IProductStockRepository productStockRepository, IWarehouseRepository warehouseRepository, ShipmentBusinessRules shipmentBusinessRules, IMapper mapper)
        {
            _shipmentRepository = shipmentRepository;
            _productStockRepository = productStockRepository;
            _warehouseRepository = warehouseRepository;
            _shipmentBusinessRules = shipmentBusinessRules;
            _mapper = mapper;
        }

        public async Task<CreatedShipmentResponse> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {
            await _shipmentBusinessRules.CheckIfSufficientProductStockExists(request.ProductId, request.WarehouseId, request.Quantity);

            await _shipmentBusinessRules.CheckIfCustomerIdExists(request.CustomerId);

            Shipment shipment = _mapper.Map<Shipment>(request);

            shipment.DeliveryStatus = Domain.Enums.DeliveryStatus.Pending;

            await _shipmentRepository.AddAsync(shipment);

            /*
            productStock.Quantity -= request.Quantity;

            await _productStockRepository.UpdateAsync(productStock);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(w => w.Id == request.WarehouseId);

            warehouse.CurrentCapacity -= request.Quantity;

            await _warehouseRepository.UpdateAsync(warehouse);
            */

            await _productStockRepository.UpdateExecuteAsync(
                predicate: ps => ps.ProductId == request.ProductId && ps.WarehouseId == request.WarehouseId,
                setPropertyCalls: p => p.SetProperty(ps => ps.Quantity,
                                                           ps => ps.Quantity - request.Quantity));

            await _warehouseRepository.UpdateExecuteAsync(
                predicate: w => w.Id == request.WarehouseId,
                setPropertyCalls: p => p.SetProperty(w => w.CurrentCapacity,
                                                           w => w.CurrentCapacity - request.Quantity));

            return _mapper.Map<CreatedShipmentResponse>(shipment);

        }
    }
}
