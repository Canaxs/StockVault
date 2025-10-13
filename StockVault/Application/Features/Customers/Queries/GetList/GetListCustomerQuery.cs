using Application.Features.Warehouses.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
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

namespace Application.Features.Customers.Queries.GetList;

public class GetListCustomerQuery:IRequest<GetListResponse<GetListCustomerListItemDto>>, ICacheableRequest
{
    public PageRequest PageRequest { get; set; }
    public string CacheKey => $"GetListCustomerQuery({PageRequest.PageIndex},{PageRequest.PageSize})";

    public bool BypassCache { get; }

    public string? CacheGroupKey => "GetCustomers";

    public TimeSpan? SlidingExpiration { get; }

    public class GetListCustomerQueryHandler : IRequestHandler<GetListCustomerQuery, GetListResponse<GetListCustomerListItemDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetListCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCustomerListItemDto>> Handle(GetListCustomerQuery request, CancellationToken cancellationToken)
        {
            Paginate<Customer> customers = await _customerRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            return _mapper.Map<GetListResponse<GetListCustomerListItemDto>>(customers);
        }
    }
}
