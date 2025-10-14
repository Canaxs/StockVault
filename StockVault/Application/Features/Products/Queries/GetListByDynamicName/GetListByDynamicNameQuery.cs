using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListByDynamicName;

public class GetListByDynamicNameQuery : IRequest<GetListResponse<GetListByDynamicNameListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public string FieldValue { get; set; }
    public string FieldOperator { get; set; }

    public string SortField { get; set; }
    public string SortDir {  get; set; }

    public class GetListByDynamicNameQueryHandler : IRequestHandler<GetListByDynamicNameQuery, GetListResponse<GetListByDynamicNameListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicNameQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicNameListItemDto>> Handle(GetListByDynamicNameQuery request, CancellationToken cancellationToken)
        {

            DynamicQuery dynamicQuery = new (new Sort(request.SortField,request.SortDir), 
                                            new Filter("Name",request.FieldOperator,request.FieldValue));

            Paginate<Product> dynamicProducts = await _productRepository.GetListByDynamicAsync(
                dynamicQuery,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            return _mapper.Map<GetListResponse<GetListByDynamicNameListItemDto>>(dynamicProducts);
        }
    }

}
