using Application.Features.Products.Rules;
using Application.Features.Warehouses.Queries.GetListShipmentSummary;
using Application.Services.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListCustomer;

public class GetListCustomerByProductIdQuery:IRequest<GetListResponse<GetListCustomerByProductIdListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class GetListCustomerByProductIdQueryHandler : IRequestHandler<GetListCustomerByProductIdQuery, GetListResponse<GetListCustomerByProductIdListItemDto>>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;

        public GetListCustomerByProductIdQueryHandler(IShipmentRepository shipmentRepository, IMapper mapper, ProductBusinessRules productBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<GetListResponse<GetListCustomerByProductIdListItemDto>> Handle(GetListCustomerByProductIdQuery request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductShouldExistWhenRequested(request.Id);

            Paginate<GetListCustomerByProductIdListItemDto> shipments = await _shipmentRepository.GetListProjectedAsync(
                predicate: s => s.ProductId == request.Id
                        && (!request.StartDate.HasValue || s.CreatedDate >= request.StartDate.Value)
                        && (!request.EndDate.HasValue || s.CreatedDate <= request.EndDate.Value),
                include: s => s.Include(s => s.Customer),
                groupBy: q => q
                .GroupBy(s => s.ProductId)
                .Select(g => new GetListCustomerByProductIdListItemDto
                {
                    Id = g.First().Id,
                    ProductId = g.First().ProductId,
                    ProductName = g.First().Product.Name,
                    CustomerId = g.First().CustomerId,
                    CustomerName = g.First().Customer.Name,
                    CustomerPhoneNumber = g.First().Customer.PhoneNumber,
                    TotalQuantity = g.Sum(x => x.Quantity)
                }),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
               );

            return _mapper.Map<GetListResponse<GetListCustomerByProductIdListItemDto>>(shipments);
        }
    }
}
