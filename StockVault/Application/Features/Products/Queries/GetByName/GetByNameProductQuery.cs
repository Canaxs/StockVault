using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetByName;

public class GetByNameProductQuery:IRequest<GetByNameProductResponse>, ICacheableRequest
{
    public string Name { get; set; }
    public string CacheKey => $"GetByProductName-{Name}";

    public bool BypassCache { get; }

    public string? CacheGroupKey => "GetProducts";

    public TimeSpan? SlidingExpiration { get; }

    public class GetByNameProductQueryHandler : IRequestHandler<GetByNameProductQuery, GetByNameProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;

        public GetByNameProductQueryHandler(IProductRepository productRepository, IMapper mapper, ProductBusinessRules productBusinessRules)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<GetByNameProductResponse> Handle(GetByNameProductQuery request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.CheckProductNameExists(request.Name);

            Product? product = await _productRepository.GetAsync(predicate: p => p.Name == request.Name, cancellationToken: cancellationToken);

            return _mapper.Map<GetByNameProductResponse>(product);
        }
    }
}
