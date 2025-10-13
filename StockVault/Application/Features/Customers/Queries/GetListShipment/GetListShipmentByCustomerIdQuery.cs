using Application.Features.Customers.Queries.GetListProduct;
using Application.Features.Customers.Rules;
using Application.Features.Warehouses.Queries.GetListShipment;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetListShipment;

public class GetListShipmentByCustomerIdQuery:IRequest<GetListResponse<GetListShipmentByCustomerIdListItemDto>>
{
    public int Id { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public PageRequest PageRequest { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class GetListShipmentByCustomerIdQueryHandler : IRequestHandler<GetListShipmentByCustomerIdQuery, GetListResponse<GetListShipmentByCustomerIdListItemDto>>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public GetListShipmentByCustomerIdQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper, CustomerBusinessRules customerBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<GetListResponse<GetListShipmentByCustomerIdListItemDto>> Handle(GetListShipmentByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CheckIfCustomerIdExists(request.Id);

            Paginate<Shipment> shipments = await _shipmentRepository.GetListAsync(
                predicate: s => s.CustomerId == request.Id && s.DeliveryStatus == request.DeliveryStatus
                        && (!request.StartDate.HasValue || s.CreatedDate >= request.StartDate.Value)
                        && (!request.EndDate.HasValue || s.CreatedDate <= request.EndDate.Value),
                include: s => s.Include(s => s.Product)
                               .Include(s => s.Warehouse)
                               .Include(s => s.Customer),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
               );

            return _mapper.Map<GetListResponse<GetListShipmentByCustomerIdListItemDto>>(shipments);
        }
    }
}
