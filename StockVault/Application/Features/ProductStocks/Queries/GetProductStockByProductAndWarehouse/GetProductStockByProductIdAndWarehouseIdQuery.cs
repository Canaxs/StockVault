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

namespace Application.Features.ProductStocks.Queries.GetProductStockByProductAndWarehouse;

public class GetProductStockByProductIdAndWarehouseIdQuery:IRequest<GetProductStockByProductIdAndWarehouseIdResponse>
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }

    public class GetProductStockByProductIdAndWarehouseIdQueryHandler : IRequestHandler<GetProductStockByProductIdAndWarehouseIdQuery, GetProductStockByProductIdAndWarehouseIdResponse>
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;
        private readonly ProductStockBusinessRules _productStockBusinessRules;

        public GetProductStockByProductIdAndWarehouseIdQueryHandler(IProductStockRepository productStockRepository, IMapper mapper, ProductStockBusinessRules productStockBusinessRules)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
            _productStockBusinessRules = productStockBusinessRules;
        }

        public async Task<GetProductStockByProductIdAndWarehouseIdResponse> Handle(GetProductStockByProductIdAndWarehouseIdQuery request, CancellationToken cancellationToken)
        {
            await _productStockBusinessRules.CheckProductStockExistsInWarehouse(request.ProductId, request.WarehouseId);

            ProductStock? productStock = await _productStockRepository.GetAsync(
                predicate: ps => ps.ProductId == request.ProductId && ps.WarehouseId == request.WarehouseId,
                include: q => q.Include(ps => ps.Product).Include(ps => ps.Warehouse),
                cancellationToken: cancellationToken
                );

            return _mapper.Map<GetProductStockByProductIdAndWarehouseIdResponse>(productStock);
        }
    }
}
