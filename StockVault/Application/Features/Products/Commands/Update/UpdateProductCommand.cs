using Application.Features.Products.Rules;
using Application.Features.Warehouses.Commands.Update;
using Application.Features.Warehouses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommand:IRequest<UpdatedProductResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdatedProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ProductBusinessRules productBusinessRules)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<UpdatedProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductNameCannotBeDuplicatedWhenInserted(request.Name);

            Product product = await _productRepository.GetAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.Name) && !string.Equals(product?.Name, request.Name, StringComparison.Ordinal))
            {
                await _productBusinessRules.ProductNameCannotBeDuplicatedWhenInserted(request.Name);
                product.Name = request.Name;
            }

            if (!string.IsNullOrWhiteSpace(request.Description) && !string.Equals(product?.Description, request.Description, StringComparison.Ordinal))
                product.Description = request.Description;

            if (request.Price.HasValue && product?.Price != request.Price)
                product.Price = request.Price.Value;

            await _productRepository.UpdateAsync(product);

            return _mapper.Map<UpdatedProductResponse>(product);
        }
    }
}
