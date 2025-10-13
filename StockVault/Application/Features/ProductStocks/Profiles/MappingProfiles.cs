using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Update;
using Application.Features.ProductStocks.Commands.Create;
using Application.Features.ProductStocks.Commands.Delete;
using Application.Features.ProductStocks.Commands.Update;
using Application.Features.ProductStocks.Queries.GetById;
using Application.Features.ProductStocks.Queries.GetList;
using Application.Features.ProductStocks.Queries.GetProductStockByProductAndWarehouse;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<ProductStock, CreateProductStockCommand>().ReverseMap();
        CreateMap<ProductStock, CreatedProductStockResponse>().ReverseMap();

        CreateMap<ProductStock, GetListProductStockListItemDto>()
            .ForMember(destinationMember: c => c.ProductName, memberOptions: opt => opt.MapFrom(c => c.Product.Name))
            .ForMember(destinationMember: c => c.WarehouseName, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Name))
            .ReverseMap();

        CreateMap<Paginate<ProductStock>, GetListResponse<GetListProductStockListItemDto>>().ReverseMap();

        CreateMap<ProductStock, DeletedProductStockResponse>().ReverseMap();
        CreateMap<ProductStock, DeleteProductStockCommand>().ReverseMap();

        CreateMap<ProductStock, UpdatedProductStockResponse>().ReverseMap();

        CreateMap<ProductStock, GetByIdProductStockResponse>()
            .ForMember(destinationMember: c => c.ProductName, memberOptions: opt => opt.MapFrom(c => c.Product.Name))
            .ForMember(destinationMember: c => c.WarehouseName, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Name))
            .ReverseMap();

        CreateMap<ProductStock, GetProductStockByProductIdAndWarehouseIdResponse>()
            .ForMember(destinationMember: c => c.ProductName, memberOptions: opt => opt.MapFrom(c => c.Product.Name))
            .ForMember(destinationMember: c => c.WarehouseName, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Name))
            .ReverseMap();
    }
}
