using Microsoft.EntityFrameworkCore;
using Application.Features.Warehouses.Queries.GetListProduct;
using Application.Features.Warehouses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Queries.GetListShipment;

public class GetListShipmentByWarehouseIdQuery:IRequest<GetListResponse<GetListShipmentByWarehouseIdListItemDto>>
{
    public int Id { get; set; }
    public PageRequest PageRequest { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class GetListShipmentByWarehouseIdQueryHandler : IRequestHandler<GetListShipmentByWarehouseIdQuery, GetListResponse<GetListShipmentByWarehouseIdListItemDto>>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public GetListShipmentByWarehouseIdQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper, WarehouseBusinessRules warehouseBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules; 
        }

        public async Task<GetListResponse<GetListShipmentByWarehouseIdListItemDto>> Handle(GetListShipmentByWarehouseIdQuery request, CancellationToken cancellationToken)
        {
            await _warehouseBusinessRules.WarehouseShouldExistWhenRequested(request.Id);

            Paginate<Shipment> shipments = await _shipmentRepository.GetListAsync(
                predicate: s => s.WarehouseId == request.Id
                        && (!request.StartDate.HasValue || s.CreatedDate >= request.StartDate.Value)
                        && (!request.EndDate.HasValue || s.CreatedDate <= request.EndDate.Value),
                include: s => s.Include(s => s.Product)
                               .Include(s => s.Warehouse)
                               .Include(s => s.Customer),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
               );

            return _mapper.Map<GetListResponse<GetListShipmentByWarehouseIdListItemDto>>(shipments);
        }
    }
}
