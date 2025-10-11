using Application.Features.Customers.Queries.GetById;
using Application.Features.Shipments.Commands.Create;
using Application.Features.Shipments.Commands.Delete;
using Application.Features.Shipments.Commands.Update;
using Application.Features.Shipments.Queries.GetById;
using Application.Features.Shipments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shipments.Profiles;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Shipment, CreatedShipmentResponse>().ReverseMap();
        CreateMap<Shipment, CreateShipmentCommand>().ReverseMap();

        CreateMap<Shipment, GetByIdShipmentResponse>().ReverseMap();

        CreateMap<Shipment, DeletedShipmentResponse>().ReverseMap();

        CreateMap<Shipment, UpdatedShipmentResponse>().ReverseMap();

        CreateMap<Shipment, GetListShipmentListItemDto>()
            .ForMember(destinationMember: c => c.ProductName, memberOptions: opt => opt.MapFrom(c => c.Product.Name))
            .ForMember(destinationMember: c => c.WarehouseName, memberOptions: opt => opt.MapFrom(c => c.Warehouse.Name))
            .ReverseMap();

        CreateMap<Paginate<Shipment>, GetListResponse<GetListShipmentListItemDto>>().ReverseMap();

    }
}
