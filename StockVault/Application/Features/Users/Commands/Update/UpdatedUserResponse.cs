using Application.Features.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Update;

public class UpdatedUserResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public ICollection<OperationClaimDto> OperationClaimDtos { get; set; }
}
