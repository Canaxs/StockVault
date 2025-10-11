using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetByName;
using Application.Features.Products.Queries.GetList;
using Application.Features.Products.Queries.GetListCustomer;
using Application.Features.Products.Queries.GetListShipment;
using Application.Features.Products.Queries.GetListWarehouse;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product, CreatedProductResponse>().ReverseMap();

        CreateMap<Product, DeletedProductResponse>().ReverseMap();

        CreateMap<Product, UpdatedProductResponse>().ReverseMap();

        CreateMap<Product, GetByIdProductResponse>().ReverseMap();

        CreateMap<Product, GetListProductListItemDto>().ReverseMap();

        CreateMap<Paginate<Product>, GetListResponse<GetListProductListItemDto>>().ReverseMap();

        CreateMap<Product, GetByNameProductResponse>().ReverseMap();

        CreateMap<ProductStock, GetListWarehouseByProductIdListItemDto>()
            .ForMember(destinationMember: c => c.WarehouseId, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Id))
            .ForMember(destinationMember: c => c.WarehouseName, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Name))
            .ForMember(destinationMember: c => c.WarehouseLocation, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Location))
            .ReverseMap();

        CreateMap<Paginate<ProductStock>, GetListResponse<GetListWarehouseByProductIdListItemDto>>().ReverseMap();

        CreateMap<Shipment, GetListShipmentByProductIdListItemDto>()
            .ForMember(destinationMember: c => c.ProductId, memberOptions: opt => opt.MapFrom(c => c.Product.Id))
            .ForMember(destinationMember: c => c.ProductName, memberOptions: opt => opt.MapFrom(c => c.Product.Name))
            .ForMember(destinationMember: c => c.WarehouseId, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Id))
            .ForMember(destinationMember: c => c.WarehouseName, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Name))
            .ForMember(destinationMember: c => c.CustomerName, memberOptions: opt => opt.MapFrom(c => c.Customer.Name))
            .ReverseMap();

        CreateMap<Paginate<Shipment>, GetListResponse<GetListShipmentByProductIdListItemDto>>().ReverseMap();

        CreateMap<Paginate<GetListCustomerByProductIdListItemDto>, GetListResponse<GetListCustomerByProductIdListItemDto>>().ReverseMap();

    }
}
