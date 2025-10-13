using Application.Features.Users.Commands.Update;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OperationClaim, OperationClaimDto>();

        CreateMap<User, UpdatedUserResponse>()
            .ForMember(destinationMember: c => c.OperationClaimDtos, memberOptions: opt => opt.MapFrom(c => c.UserOperationClaims.Select(uoc => uoc.OperationClaim)));

        CreateMap<User, GetByIdUserResponse>()
            .ForMember(destinationMember: c => c.OperationClaimDtos, memberOptions: opt => opt.MapFrom(c => c.UserOperationClaims.Select(uoc => uoc.OperationClaim)));

        CreateMap<User, GetListUserListItemDto>()
            .ForMember(destinationMember: c => c.OperationClaimDtos, memberOptions: opt => opt.MapFrom(c => c.UserOperationClaims.Select(uoc => uoc.OperationClaim)));

        CreateMap<Paginate<User>, GetListResponse<GetListUserListItemDto>>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
