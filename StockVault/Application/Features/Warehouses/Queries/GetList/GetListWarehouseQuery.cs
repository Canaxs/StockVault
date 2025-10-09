using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Queries.GetList;

public class GetListWarehouseQuery : IRequest<GetListResponse<GetListWarehouseListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListWarehouseQueryHandler : IRequestHandler<GetListWarehouseQuery, GetListResponse<GetListWarehouseListItemDto>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetListWarehouseQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListWarehouseListItemDto>> Handle(GetListWarehouseQuery request, CancellationToken cancellationToken)
        {
            Paginate<Warehouse> warehouses = await _warehouseRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            return _mapper.Map<GetListResponse<GetListWarehouseListItemDto>>(warehouses);

        }
    }
}
