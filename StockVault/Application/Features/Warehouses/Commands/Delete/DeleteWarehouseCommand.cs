using Application.Features.Warehouses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Commands.Delete;

public class DeleteWarehouseCommand : IRequest<DeletedWarehouseResponse>, ILoggableRequest
{
    public int Id { get; set; }

    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, DeletedWarehouseResponse>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public DeleteWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMapper mapper, WarehouseBusinessRules warehouseBusinessRules)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
        }

        public async Task<DeletedWarehouseResponse> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _warehouseBusinessRules.WarehouseShouldExistWhenRequested(request.Id);
            await _warehouseBusinessRules.CheckIfWarehouseHasNoStockBeforeDeletion(request.Id);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == request.Id, cancellationToken: cancellationToken);

            await _warehouseRepository.DeleteAsync(warehouse);

            return _mapper.Map<DeletedWarehouseResponse>(warehouse);
        }
    }
}
