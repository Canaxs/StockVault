using Application.Features.ProductStocks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Commands.Update;

public class UpdateProductStockCommand:IRequest<UpdatedProductStockResponse>, ITransactionalRequest, ILoggableRequest
{
    public int Id { get; set;}
    public int Quantity { get; set;}

    public class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommand, UpdatedProductStockResponse>
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly ProductStockBusinessRules _productStockBusinessRules;

        public UpdateProductStockCommandHandler(IProductStockRepository productStockRepository, IWarehouseRepository warehouseRepository, IMapper mapper, ProductStockBusinessRules productStockBusinessRules)
        {
            _productStockRepository = productStockRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _productStockBusinessRules = productStockBusinessRules;
        }

        public async Task<UpdatedProductStockResponse> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            await _productStockBusinessRules.CheckIfProductStockIdExists(request.Id);

            ProductStock? productStock = await _productStockRepository.GetAsync(predicate: ps => ps.Id == request.Id, cancellationToken: cancellationToken);

            int lastQuantity = productStock.Quantity;

            if (request.Quantity > lastQuantity)
                await _productStockBusinessRules.CheckWarehouseHasEnoughCapacity(productStock.WarehouseId, (request.Quantity - lastQuantity));

            productStock.Quantity = request.Quantity;

            await _productStockRepository.UpdateAsync(productStock);
            /*

            Warehouse warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == productStock.WarehouseId);

            warehouse.CurrentCapacity += (request.Quantity - lastQuantity);

            await _warehouseRepository.UpdateAsync(warehouse);
            */

            await _warehouseRepository.UpdateExecuteAsync(
                predicate: w => w.Id == productStock.WarehouseId,
                setPropertyCalls: p => p.SetProperty(w => w.CurrentCapacity,
                                                           w => w.CurrentCapacity + (request.Quantity - lastQuantity) ));

            return _mapper.Map<UpdatedProductStockResponse>(productStock);
        }
    }
}
