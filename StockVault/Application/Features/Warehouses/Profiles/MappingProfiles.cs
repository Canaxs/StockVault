using Application.Features.Warehouses.Commands.Create;
using Application.Features.Warehouses.Commands.Delete;
using Application.Features.Warehouses.Commands.Update;
using Application.Features.Warehouses.Queries.GetById;
using Application.Features.Warehouses.Queries.GetList;
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
    }
}
