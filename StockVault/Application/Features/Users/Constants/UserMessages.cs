using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Constants;

public class UserMessages
{
    public const string UserAlreadyExists = "This username is already taken. Please choose a different one.";
    public const string UserNotFound = "User not found.";
    public const string OperationClaimNotFound = "Operation claim with the given ID was not found.";
    public const string UserAlreadyHasOperationClaim = "This user already has the specified role.";
}
