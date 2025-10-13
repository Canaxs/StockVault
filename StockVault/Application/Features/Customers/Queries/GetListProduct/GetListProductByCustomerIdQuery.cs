using Application.Features.Customers.Queries.GetListShipment;
using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetListProduct;

public class GetListProductByCustomerIdQuery:IRequest<GetListResponse<GetListProductByCustomerIdListItemDto>>
{
    public int Id { get; set; }
    public PageRequest PageRequest { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class GetListProductByCustomerIdQueryHandler : IRequestHandler<GetListProductByCustomerIdQuery, GetListResponse<GetListProductByCustomerIdListItemDto>>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public GetListProductByCustomerIdQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper, CustomerBusinessRules customerBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<GetListResponse<GetListProductByCustomerIdListItemDto>> Handle(GetListProductByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CheckIfCustomerIdExists(request.Id);

            Paginate<GetListProductByCustomerIdListItemDto> shipments = await _shipmentRepository.GetListProjectedAsync(
                predicate: s => s.CustomerId == request.Id && s.DeliveryStatus != Domain.Enums.DeliveryStatus.Failed
                        && (!request.StartDate.HasValue || s.CreatedDate >= request.StartDate.Value)
                        && (!request.EndDate.HasValue || s.CreatedDate <= request.EndDate.Value),
                include: s => s.Include(s => s.Product).Include(s => s.Customer),
                groupBy: q => q.GroupBy(s => s.ProductId)
                .Select(g => new GetListProductByCustomerIdListItemDto
                {
                    Id = g.Key,
                    CustomerName = g.FirstOrDefault().Customer.Name,
                    ProductId = g.FirstOrDefault().Product.Id,
                    ProductName = g.FirstOrDefault().Product.Name,
                    ProductDescription = g.FirstOrDefault().Product.Description,
                    ProductPrice = g.FirstOrDefault().Product.Price,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalPrice = g.Sum(x => x.Quantity * x.Product.Price)
                }),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
               );

            return _mapper.Map<GetListResponse<GetListProductByCustomerIdListItemDto>>(shipments);
        }
    }
}
