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

namespace Application.Features.Warehouses.Commands.Create;

public class CreateWarehouseCommand:IRequest<CreatedWarehouseResponse>, ILoggableRequest
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int MaxCapacity { get; set; }

    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, CreatedWarehouseResponse>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMapper mapper, WarehouseBusinessRules warehouseBusinessRules)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
        }

        public async Task<CreatedWarehouseResponse> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _warehouseBusinessRules.WarehouseNameCannotBeDuplicatedWhenInserted(request.Name);

            Warehouse warehouse = _mapper.Map<Warehouse>(request);

            await _warehouseRepository.AddAsync(warehouse);

            return _mapper.Map<CreatedWarehouseResponse>(warehouse);

        }
    }
}
