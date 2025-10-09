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

namespace Application.Features.Warehouses.Queries.GetById;

public class GetByIdWarehouseQuery : IRequest<GetByIdWarehouseResponse>
{
    public int Id { get; set; }

    public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdWarehouseQuery, GetByIdWarehouseResponse>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public GetByIdBrandQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper, WarehouseBusinessRules warehouseBusinessRules)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
        }

        public async Task<GetByIdWarehouseResponse> Handle(GetByIdWarehouseQuery request, CancellationToken cancellationToken)
        {
            await _warehouseBusinessRules.WarehouseShouldExistWhenRequested(request.Id);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == request.Id,cancellationToken: cancellationToken);

            return _mapper.Map<GetByIdWarehouseResponse>(warehouse);

        }
    }
}
