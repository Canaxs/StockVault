using Application.Features.Shipments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Commands.Update;

public class UpdateShipmentCommand:IRequest<UpdatedShipmentResponse>, ITransactionalRequest
{
    public int Id { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }

    public class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommand, UpdatedShipmentResponse>
    {

        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly ShipmentBusinessRules _shipmentBusinessRules;
        private readonly IProductStockRepository _productStockRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public UpdateShipmentCommandHandler(IShipmentRepository shipmentRepository, IMapper mapper, ShipmentBusinessRules shipmentBusinessRules, IProductStockRepository productStockRepository, IWarehouseRepository warehouseRepository)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _shipmentBusinessRules = shipmentBusinessRules;
            _productStockRepository = productStockRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<UpdatedShipmentResponse> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
        {
            await _shipmentBusinessRules.CheckIfShipmentExistsAndIsPending(request.Id);

            Shipment? shipment = await _shipmentRepository.GetAsync(s => s.Id == request.Id);

            shipment.DeliveryStatus = request.DeliveryStatus;

            await _shipmentRepository.UpdateAsync(shipment);

            if(shipment.DeliveryStatus == DeliveryStatus.Failed)
            {
                await _shipmentBusinessRules.CheckIfProductStockExists(shipment.ProductId, shipment.WarehouseId);
                await _shipmentBusinessRules.CheckIfWarehouseHasEnoughCapacity(shipment.WarehouseId, shipment.Quantity);

                ProductStock? productStock = await _productStockRepository.GetAsync(
                    predicate: ps => ps.ProductId == shipment.ProductId && ps.WarehouseId == shipment.WarehouseId
                );

                productStock.Quantity += shipment.Quantity;
                await _productStockRepository.UpdateAsync(productStock);

                Warehouse? warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == shipment.WarehouseId);

                warehouse.CurrentCapacity += shipment.Quantity;
                await _warehouseRepository.UpdateAsync(warehouse);
            }

            return _mapper.Map<UpdatedShipmentResponse>(shipment);
        }
    }
}
