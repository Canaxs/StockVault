using Application.Features.Products.Rules;
using Application.Features.Warehouses.Commands.Create;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Create;

public class CreateProductCommand: IRequest<CreatedProductResponse>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductResponse>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ProductBusinessRules productBusinessRules)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<CreatedProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductNameCannotBeDuplicatedWhenInserted(request.Name);

            Product product = _mapper.Map<Product>(request);

            await _productRepository.AddAsync(product);

            return _mapper.Map<CreatedProductResponse>(product);

        }
    }
}
