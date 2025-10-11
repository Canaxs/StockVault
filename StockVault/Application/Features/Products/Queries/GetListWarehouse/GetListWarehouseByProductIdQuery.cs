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

namespace Application.Features.Products.Queries.GetListWarehouse;

public class GetListWarehouseByProductIdQuery:IRequest<GetListResponse<GetListWarehouseByProductIdListItemDto>>
{
    public PageRequest PageRequest;
    public int Id { get; set; }

    public class GetListWarehouseByProductIdQueryHandler : IRequestHandler<GetListWarehouseByProductIdQuery, GetListResponse<GetListWarehouseByProductIdListItemDto>>
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;

        public GetListWarehouseByProductIdQueryHandler(IProductStockRepository productStockRepository, IMapper mapper, ProductBusinessRules productBusinessRules)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<GetListResponse<GetListWarehouseByProductIdListItemDto>> Handle(GetListWarehouseByProductIdQuery request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductShouldExistWhenRequested(request.Id);

            Paginate<ProductStock> productStocks = await _productStockRepository.GetListAsync(
                predicate: ps => ps.ProductId == request.Id,
                include: ps => ps.Include(ps => ps.Warehouse),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            return _mapper.Map<GetListResponse<GetListWarehouseByProductIdListItemDto>>(productStocks);
        }
    }
}
