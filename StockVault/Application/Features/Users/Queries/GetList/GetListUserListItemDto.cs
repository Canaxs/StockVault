using Application.Features.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserListItemDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public ICollection<OperationClaimDto> OperationClaimDtos { get; set; }
}
