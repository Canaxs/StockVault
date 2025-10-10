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

namespace Application.Features.Shipments.Queries.GetById;

public class GetByIdShipmentQuery : IRequest<GetByIdShipmentResponse>
{
    public int Id { get; set; }

    public class GetByIdShipmentQueryHandler : IRequestHandler<GetByIdShipmentQuery, GetByIdShipmentResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly ShipmentBusinessRules _shipmentBusinessRules;

        public GetByIdShipmentQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper,ShipmentBusinessRules shipmentBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _shipmentBusinessRules = shipmentBusinessRules;
        }

        public async Task<GetByIdShipmentResponse> Handle(GetByIdShipmentQuery request, CancellationToken cancellationToken)
        {
            Shipment? shipment = await _shipmentRepository.GetAsync(s => s.Id == request.Id);

            return _mapper.Map<GetByIdShipmentResponse>(shipment);
        }
    }
}
