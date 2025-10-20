using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Queries.GetByClaims;

public class GetByClaimsResponse
{
    public string? Username { get; set; }
    public List<string>? Roles { get; set; }
}
