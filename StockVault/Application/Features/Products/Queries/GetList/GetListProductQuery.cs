using Application.Features.Products.Rules;
using Application.Features.Warehouses.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
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

namespace Application.Features.Products.Queries.GetList;

public class GetListProductQuery:IRequest<GetListResponse<GetListProductListItemDto>>, ICacheableRequest
{
    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListProductQuery({PageRequest.PageIndex},{PageRequest.PageSize})";

    public bool BypassCache {get;}

    public string? CacheGroupKey => "GetProducts";

    public TimeSpan? SlidingExpiration { get; }

    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, GetListResponse<GetListProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProductListItemDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            Paginate<Product> products = await _productRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            return _mapper.Map<GetListResponse<GetListProductListItemDto>>(products);
        }
    }
}
