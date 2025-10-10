using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetList;
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
    }
}
