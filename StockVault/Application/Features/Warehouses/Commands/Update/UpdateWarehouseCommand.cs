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

namespace Application.Features.Warehouses.Commands.Update;

public class UpdateWarehouseCommand : IRequest<UpdatedWarehouseResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public int? MaxCapacity { get; set; }

    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, UpdatedWarehouseResponse>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public UpdateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMapper mapper, WarehouseBusinessRules warehouseBusinessRules)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
        }

        public async Task<UpdatedWarehouseResponse> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _warehouseBusinessRules.WarehouseShouldExistWhenRequested(request.Id);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(w => w.Id == request.Id,cancellationToken: cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.Name) && !string.Equals(warehouse?.Name, request.Name, StringComparison.Ordinal))
            {
                await _warehouseBusinessRules.WarehouseNameCannotBeDuplicatedWhenInserted(request.Name);
                warehouse.Name = request.Name;
            }

            if(!string.IsNullOrWhiteSpace(request.Location) && !string.Equals(warehouse?.Location, request.Location, StringComparison.Ordinal))
                warehouse.Location = request.Location;

            if (request.MaxCapacity.HasValue && warehouse?.MaxCapacity != request.MaxCapacity)
            {
                _warehouseBusinessRules.CheckIfMaxCapacityIsValid(request.MaxCapacity.Value, warehouse);
                warehouse.MaxCapacity = request.MaxCapacity.Value;
            }
                

            await _warehouseRepository.UpdateAsync(warehouse);

            return _mapper.Map<UpdatedWarehouseResponse>(warehouse);
        }
    }



}
