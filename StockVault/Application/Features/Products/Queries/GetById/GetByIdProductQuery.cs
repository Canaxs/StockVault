using Application.Features.Products.Rules;
using Application.Features.Warehouses.Queries.GetById;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetById;

public class GetByIdProductQuery:IRequest<GetByIdProductResponse>,ICacheableRequest
{
    public int Id { get; set; }
    public string CacheKey => $"GetByProductId-{Id}";

    public bool BypassCache { get; }

    public string? CacheGroupKey => "GetProducts";

    public TimeSpan? SlidingExpiration { get; }

    public class GetByIdProductQueryHandler: IRequestHandler<GetByIdProductQuery, GetByIdProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;

        public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper, ProductBusinessRules productBusinessRules)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<GetByIdProductResponse> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductShouldExistWhenRequested(request.Id);

            Product? product = await _productRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);

            return _mapper.Map<GetByIdProductResponse>(product);
        }
    }
}
