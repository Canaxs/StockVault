using Application.Features.ProductStocks.Rules;
using Application.Features.Warehouses.Commands.Delete;
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

namespace Application.Features.ProductStocks.Commands.Delete;

public class DeleteProductStockCommand: IRequest<DeletedProductStockResponse>, ITransactionalRequest, ILoggableRequest
{
    public int Id { get; set; }

    public class DeleteProductStockCommandHandler : IRequestHandler<DeleteProductStockCommand, DeletedProductStockResponse>
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly ProductStockBusinessRules _productStockBusinessRules;

        public DeleteProductStockCommandHandler(IProductStockRepository productStockRepository, IMapper mapper, ProductStockBusinessRules productStockBusinessRules, IWarehouseRepository warehouseRepository)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
            _productStockBusinessRules = productStockBusinessRules;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<DeletedProductStockResponse> Handle(DeleteProductStockCommand request, CancellationToken cancellationToken)
        {
            await _productStockBusinessRules.CheckIfProductStockIdExists(request.Id);

            ProductStock? productStock = await _productStockRepository.GetAsync(predicate: ps => ps.Id == request.Id, cancellationToken: cancellationToken);

            int warehouseId = productStock.WarehouseId;
            int quantity = productStock.Quantity;

            await _productStockRepository.DeleteAsync(productStock);

            /*

            Warehouse warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == warehouseId);

            warehouse.CurrentCapacity -= quantity;

            await _warehouseRepository.UpdateAsync(warehouse);
            */

            await _warehouseRepository.UpdateExecuteAsync(
                predicate: w => w.Id == warehouseId,
                setPropertyCalls: p => p.SetProperty(w => w.CurrentCapacity,
                                                           w => w.CurrentCapacity - quantity));

            return _mapper.Map<DeletedProductStockResponse>(productStock);
        }
    }
}
