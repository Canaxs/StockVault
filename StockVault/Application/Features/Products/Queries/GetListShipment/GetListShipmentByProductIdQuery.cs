using Application.Features.Products.Rules;
using Application.Features.Warehouses.Rules;
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

namespace Application.Features.Products.Queries.GetListShipment;

public class GetListShipmentByProductIdQuery: IRequest<GetListResponse<GetListShipmentByProductIdListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class GetListShipmentByProductIdQueryHandler : IRequestHandler<GetListShipmentByProductIdQuery, GetListResponse<GetListShipmentByProductIdListItemDto>>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;

        public GetListShipmentByProductIdQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper, ProductBusinessRules productBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<GetListResponse<GetListShipmentByProductIdListItemDto>> Handle(GetListShipmentByProductIdQuery request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductShouldExistWhenRequested(request.Id);

            Paginate<Shipment> shipments = await _shipmentRepository.GetListAsync(
                predicate: s => s.ProductId == request.Id
                        && (!request.StartDate.HasValue || s.CreatedDate >= request.StartDate.Value)
                        && (!request.EndDate.HasValue || s.CreatedDate <= request.EndDate.Value),
                include: s => s.Include(s => s.Product)
                               .Include(s => s.Warehouse)
                               .Include(s => s.Customer),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
               );

            return _mapper.Map<GetListResponse<GetListShipmentByProductIdListItemDto>>(shipments);
        }
    }
}
