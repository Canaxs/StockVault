using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Queries.GetList;

public class GetListShipmentQuery:IRequest<GetListResponse<GetListShipmentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListShipmentQueryHandler : IRequestHandler<GetListShipmentQuery, GetListResponse<GetListShipmentListItemDto>>
    {

        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;

        public GetListShipmentQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListShipmentListItemDto>> Handle(GetListShipmentQuery request, CancellationToken cancellationToken)
        {
            Paginate<Shipment> shipments = await _shipmentRepository.GetListAsync(
                include: s => s.Include(s => s.Product).Include(s => s.Warehouse),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            return _mapper.Map<GetListResponse<GetListShipmentListItemDto>>(shipments);
         }
    }
}
