using Application.Features.Warehouses.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Queries.GetList;

public class GetListProductStockQuery : IRequest<GetListResponse<GetListProductStockListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListProductStockQueryHandler : IRequestHandler<GetListProductStockQuery, GetListResponse<GetListProductStockListItemDto>>
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;

        public GetListProductStockQueryHandler(IProductStockRepository productStockRepository, IMapper mapper)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProductStockListItemDto>> Handle(GetListProductStockQuery request, CancellationToken cancellationToken)
        {
            Paginate<ProductStock> productStock = await _productStockRepository.GetListAsync(
                include: ps => ps.Include(ps => ps.Warehouse).Include(ps => ps.Product),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );


            return _mapper.Map<GetListResponse<GetListProductStockListItemDto>>(productStock);
        }
    }
}
