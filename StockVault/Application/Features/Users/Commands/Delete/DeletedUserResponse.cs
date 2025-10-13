using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Delete;

public class DeletedUserResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
}
