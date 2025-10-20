using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Application.Features.Customers.Queries.GetById;
using Application.Features.Customers.Queries.GetList;
using Application.Features.Customers.Queries.GetListProduct;
using Application.Features.Customers.Queries.GetListShipment;
using Application.Features.Warehouses.Queries.GetList;
using Application.Features.Warehouses.Queries.GetListShipment;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
        CreateMap<Customer, CreatedCustomerResponse>().ReverseMap();

        CreateMap<Customer, UpdatedCustomerResponse>().ReverseMap();
        CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();

        CreateMap<Customer, DeletedCustomerResponse>().ReverseMap();

        CreateMap<Customer, GetByIdCustomerResponse>().ReverseMap();

        CreateMap<Customer, GetListCustomerListItemDto>().ReverseMap();
        CreateMap<Paginate<Customer>, GetListResponse<GetListCustomerListItemDto>>().ReverseMap();

        CreateMap<Shipment, GetListShipmentByCustomerIdListItemDto>()
            .ForMember(destinationMember: c => c.ProductId, memberOptions: opt => opt.MapFrom(c => c.Product.Id))
            .ForMember(destinationMember: c => c.ProductName, memberOptions: opt => opt.MapFrom(c => c.Product.Name))
            .ForMember(destinationMember: c => c.WarehouseId, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Id))
            .ForMember(destinationMember: c => c.WarehouseName, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Name))
            .ForMember(destinationMember: c => c.CustomerName, memberOptions: opt => opt.MapFrom(c => c.Customer.Name))
            .ReverseMap();

        CreateMap<Paginate<GetListProductByCustomerIdListItemDto>, GetListResponse<GetListProductByCustomerIdListItemDto>>().ReverseMap();

        CreateMap<Paginate<Shipment>, GetListResponse<GetListShipmentByCustomerIdListItemDto>>().ReverseMap();

    }
}
