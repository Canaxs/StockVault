using Application.Features.Warehouses.Queries.GetList;
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

namespace Application.Features.Warehouses.Queries.GetListProduct;

public class GetListProductByWarehouseIdQuery: IRequest<GetListResponse<GetListProductByWarehouseIdListItemDto>>
{
    public int Id { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetListProductByWarehouseIdQueryHandler : IRequestHandler<GetListProductByWarehouseIdQuery, GetListResponse<GetListProductByWarehouseIdListItemDto>>
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public GetListProductByWarehouseIdQueryHandler(IProductStockRepository productStockRepository, IMapper mapper, WarehouseBusinessRules warehouseBusinessRules)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
        }

        public async Task<GetListResponse<GetListProductByWarehouseIdListItemDto>> Handle(GetListProductByWarehouseIdQuery request, CancellationToken cancellationToken)
        {
            await _warehouseBusinessRules.WarehouseShouldExistWhenRequested(request.Id);

            Paginate<ProductStock> productStocks = await _productStockRepository.GetListAsync(
                predicate: w => w.WarehouseId == request.Id,
                include: w => w.Include(w => w.Product),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
               );

            return _mapper.Map<GetListResponse<GetListProductByWarehouseIdListItemDto>>(productStocks);

        }
    }
}
