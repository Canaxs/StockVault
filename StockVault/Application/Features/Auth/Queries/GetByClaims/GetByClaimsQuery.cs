using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Core.Security.Extensions;
using System.Security.Claims;
using System.Data;

namespace Application.Features.Auth.Queries.GetByClaims;

public class GetByClaimsQuery : IRequest<GetByClaimsResponse>
{
    public class GetByClaimsQueryHandler : IRequestHandler<GetByClaimsQuery, GetByClaimsResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetByClaimsQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetByClaimsResponse> Handle(GetByClaimsQuery request, CancellationToken cancellationToken)
        {
            List<string>? userRoleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            string? username = _httpContextAccessor.HttpContext.User.GetUsername();

            return new GetByClaimsResponse
            {
                Username = username,
                Roles = userRoleClaims
            };

        }
    }
}
