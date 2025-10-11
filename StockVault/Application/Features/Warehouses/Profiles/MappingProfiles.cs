using Application.Features.Warehouses.Commands.Create;
using Application.Features.Warehouses.Commands.Delete;
using Application.Features.Warehouses.Commands.Update;
using Application.Features.Warehouses.Queries.GetById;
using Application.Features.Warehouses.Queries.GetList;
using Application.Features.Warehouses.Queries.GetListProduct;
using Application.Features.Warehouses.Queries.GetListShipment;
using Application.Features.Warehouses.Queries.GetListShipmentSummary;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Warehouses.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Warehouse, CreateWarehouseCommand>().ReverseMap();
        CreateMap<Warehouse, CreatedWarehouseResponse>().ReverseMap();

        CreateMap<Warehouse, DeletedWarehouseResponse>().ReverseMap();

        CreateMap<Warehouse, UpdatedWarehouseResponse>().ReverseMap();

        CreateMap<Warehouse, GetByIdWarehouseResponse>().ReverseMap();

        CreateMap<Warehouse, GetListWarehouseListItemDto>().ReverseMap();
        CreateMap<Paginate<Warehouse>, GetListResponse<GetListWarehouseListItemDto>>().ReverseMap();

        CreateMap<ProductStock, GetListProductByWarehouseIdListItemDto>()
            .ForMember(destinationMember: c => c.ProductId, memberOptions: opt => opt.MapFrom(c => c.Product.Id))
            .ForMember(destinationMember: c => c.ProductName, memberOptions: opt => opt.MapFrom(c => c.Product.Name))
            .ForMember(destinationMember: c => c.ProductDescription, memberOptions: opt => opt.MapFrom(c => c.Product.Description))
            .ForMember(destinationMember: c => c.ProductPrice, memberOptions: opt => opt.MapFrom(c => c.Product.Price))
            .ReverseMap();

        CreateMap<Paginate<ProductStock>, GetListResponse<GetListProductByWarehouseIdListItemDto>>().ReverseMap();

        //CreateMap<Paginate<GetListProductByWarehouseIdListItemDto>, GetListResponse<GetListProductByWarehouseIdListItemDto>>().ReverseMap();

        CreateMap<Shipment, GetListShipmentByWarehouseIdListItemDto>()
            .ForMember(destinationMember: c => c.ProductId, memberOptions: opt => opt.MapFrom(c => c.Product.Id))
            .ForMember(destinationMember: c => c.ProductName, memberOptions: opt => opt.MapFrom(c => c.Product.Name))
            .ForMember(destinationMember: c => c.WarehouseId, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Id))
            .ForMember(destinationMember: c => c.WarehouseName, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Name))
            .ForMember(destinationMember: c => c.CustomerName, memberOptions: opt => opt.MapFrom(c => c.Customer.Name))
            .ReverseMap();

        CreateMap<Paginate<Shipment>, GetListResponse<GetListShipmentByWarehouseIdListItemDto>>().ReverseMap();

        CreateMap<Paginate<GetListShipmentSummaryByWarehouseIdListItemDto>, GetListResponse<GetListShipmentSummaryByWarehouseIdListItemDto>>().ReverseMap();

    }
}
