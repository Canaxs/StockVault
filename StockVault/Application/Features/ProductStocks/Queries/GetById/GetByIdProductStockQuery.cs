using Application.Features.ProductStocks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Queries.GetById;

public class GetByIdProductStockQuery: IRequest<GetByIdProductStockResponse>
{
    public int Id { get; set; }

    public class GetByIdProductStockQueryHandler : IRequestHandler<GetByIdProductStockQuery, GetByIdProductStockResponse>
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;
        private readonly ProductStockBusinessRules _productStockBusinessRules;

        public GetByIdProductStockQueryHandler(IProductStockRepository productStockRepository, IMapper mapper, ProductStockBusinessRules productStockBusinessRules)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
            _productStockBusinessRules = productStockBusinessRules;
        }

        public async Task<GetByIdProductStockResponse> Handle(GetByIdProductStockQuery request, CancellationToken cancellationToken)
        {
            await _productStockBusinessRules.CheckIfProductStockIdExists(request.Id);

            ProductStock? productStock = await _productStockRepository.GetAsync(
                predicate: ps => ps.Id == request.Id,
                include: ps => ps.Include(ps=>ps.Product).Include(ps => ps.Warehouse),
                cancellationToken: cancellationToken
                );

            return _mapper.Map<GetByIdProductStockResponse>(productStock);
        }
    }
}
