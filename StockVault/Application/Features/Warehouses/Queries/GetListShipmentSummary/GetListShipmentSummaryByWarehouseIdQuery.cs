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

namespace Application.Features.Warehouses.Queries.GetListShipmentSummary;

public class GetListShipmentSummaryByWarehouseIdQuery:IRequest<GetListResponse<GetListShipmentSummaryByWarehouseIdListItemDto>>
{
    public int Id { get; set; }
    public PageRequest PageRequest { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }


    public class GetListShipmentSummaryByWarehouseIdQueryHandler : IRequestHandler<GetListShipmentSummaryByWarehouseIdQuery, GetListResponse<GetListShipmentSummaryByWarehouseIdListItemDto>>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public GetListShipmentSummaryByWarehouseIdQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper, WarehouseBusinessRules warehouseBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
        }

        public async Task<GetListResponse<GetListShipmentSummaryByWarehouseIdListItemDto>> Handle(GetListShipmentSummaryByWarehouseIdQuery request, CancellationToken cancellationToken)
        {
            await _warehouseBusinessRules.WarehouseShouldExistWhenRequested(request.Id);

            Paginate<GetListShipmentSummaryByWarehouseIdListItemDto> shipments = await _shipmentRepository.GetListProjectedAsync(
                predicate: s => s.WarehouseId == request.Id
                        && (!request.StartDate.HasValue || s.CreatedDate >= request.StartDate.Value)
                        && (!request.EndDate.HasValue || s.CreatedDate <= request.EndDate.Value),
                include: s => s.Include(s => s.Product),
                groupBy: q => q
                .GroupBy(s => s.ProductId)
                .Select(g => new GetListShipmentSummaryByWarehouseIdListItemDto
                {
                    ProductId = g.Key,
                    ProductName = g.First().Product.Name,
                    ProductDescription = g.First().Product.Description,
                    ProductPrice = g.First().Product.Price,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalPrice = g.Sum(x => x.Quantity * x.Product.Price)
                }),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
               );

            return _mapper.Map<GetListResponse<GetListShipmentSummaryByWarehouseIdListItemDto>>(shipments);
        }
    }

}
