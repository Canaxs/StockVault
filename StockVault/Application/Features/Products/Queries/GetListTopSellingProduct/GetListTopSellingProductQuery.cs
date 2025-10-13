using Application.Features.Products.Rules;
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

namespace Application.Features.Products.Queries.GetListTopSellingProduct;

public class GetListTopSellingProductQuery:IRequest<GetListResponse<GetListTopSellingProductListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class GetListTopSellingProductQueryHandler : IRequestHandler<GetListTopSellingProductQuery, GetListResponse<GetListTopSellingProductListItemDto>>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;

        public GetListTopSellingProductQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper, ProductBusinessRules productBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<GetListResponse<GetListTopSellingProductListItemDto>> Handle(GetListTopSellingProductQuery request, CancellationToken cancellationToken)
        {

            Paginate<GetListTopSellingProductListItemDto> paginate = await _shipmentRepository.GetListProjectedAsync<GetListTopSellingProductListItemDto>(
                predicate: s => s.DeliveryStatus != Domain.Enums.DeliveryStatus.Failed
                && (!request.StartDate.HasValue || s.CreatedDate >= request.StartDate.Value) 
                && (!request.EndDate.HasValue || s.CreatedDate <= request.EndDate.Value),
                include: q => q.Include(s => s.Product),
                groupBy: q => q
                .GroupBy(s => s.ProductId)
                .Select(g => new GetListTopSellingProductListItemDto
                {
                    ProductId = g.FirstOrDefault().Product.Id,
                    ProductName = g.FirstOrDefault().Product.Name,
                    Price = g.FirstOrDefault().Product.Price,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalPrice = g.Sum(x => x.Quantity * x.Product.Price)
                }).OrderByDescending(g => g.TotalQuantity),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            return _mapper.Map<GetListResponse<GetListTopSellingProductListItemDto>>(paginate);
        }
    }
}
