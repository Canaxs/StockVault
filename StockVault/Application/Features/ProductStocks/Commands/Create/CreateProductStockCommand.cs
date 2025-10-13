using Application.Features.ProductStocks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Commands.Create;

public class CreateProductStockCommand:IRequest<CreatedProductStockResponse>,ITransactionalRequest
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }

    public class CreateProductStockCommandHandler:IRequestHandler<CreateProductStockCommand, CreatedProductStockResponse>
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly ProductStockBusinessRules _productStockBusinessRules;

        public CreateProductStockCommandHandler(IProductStockRepository productStockRepository, IMapper mapper, ProductStockBusinessRules productStockBusinessRules, IWarehouseRepository warehouseRepository)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
            _productStockBusinessRules = productStockBusinessRules;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<CreatedProductStockResponse> Handle(CreateProductStockCommand request, CancellationToken cancellationToken)
        {
            await _productStockBusinessRules.CheckProductWarehouseUniqueness(request.ProductId, request.WarehouseId);
            await _productStockBusinessRules.CheckWarehouseHasEnoughCapacity(request.WarehouseId, request.Quantity);

            ProductStock productStock = _mapper.Map<ProductStock>(request);

            await _productStockRepository.AddAsync(productStock);

            Warehouse warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == request.WarehouseId);

            warehouse.CurrentCapacity += productStock.Quantity;

            await _warehouseRepository.UpdateAsync(warehouse);

            return _mapper.Map<CreatedProductStockResponse>(productStock);
        }
    }
}
