using Application.Features.Customers.Queries.GetById;
using Application.Features.Shipments.Commands.Create;
using AutoMapper;
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

        CreateMap<Shipment, GetByIdCustomerResponse>().ReverseMap();
    }
}
