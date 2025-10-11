using Application.Features.Shipments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Commands.Delete;

public class DeleteShipmentCommand:IRequest<DeletedShipmentResponse>
{
    public int Id { get; set; }

    public class DeleteShipmentCommandHandler : IRequestHandler<DeleteShipmentCommand, DeletedShipmentResponse>
    {

        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly ShipmentBusinessRules _shipmentBusinessRules;

        public DeleteShipmentCommandHandler(IShipmentRepository shipmentRepository, IMapper mapper, ShipmentBusinessRules shipmentBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _shipmentBusinessRules = shipmentBusinessRules;
        }

        public async Task<DeletedShipmentResponse> Handle(DeleteShipmentCommand request, CancellationToken cancellationToken)
        {
            await _shipmentBusinessRules.CheckIfShipmentIdExists(request.Id);

            Shipment? shipment = await _shipmentRepository.GetAsync(s => s.Id == request.Id);

            await _shipmentRepository.DeleteAsync(shipment);

            return _mapper.Map<DeletedShipmentResponse>(shipment);
        }
    }
}
