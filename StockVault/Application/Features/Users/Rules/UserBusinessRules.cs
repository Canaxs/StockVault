using Application.Features.Auth.Constants;
using Application.Features.Users.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules;

public class UserBusinessRules:BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserBusinessRules(IUserRepository userRepository, IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userRepository = userRepository;
        _operationClaimRepository = operationClaimRepository;
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task CheckIfUsernameExists(string username)
    {
        bool exists = await _userRepository.AnyAsync(u => u.Username.ToLower() == username.ToLower());

        if (exists)
            throw new BusinessException(UserMessages.UserAlreadyExists);
    }

    public async Task CheckIfIdExists(int id)
    {
        bool exists = await _userRepository.AnyAsync(u => u.Id == id);

        if (!exists)
            throw new NotFoundException(UserMessages.UserNotFound);
    }

    public async Task CheckIfUsernameExists(string username,int id)
    {
        bool exists = await _userRepository.AnyAsync(u => u.Username.ToLower() == username.ToLower() && u.Id != id);

        if (exists)
            throw new BusinessException(UserMessages.UserAlreadyExists);
    }
    public async Task CheckIfOperationClaimExists(int operationClaimId)
    {
        bool exists = await _operationClaimRepository.AnyAsync(o => o.Id == operationClaimId);

        if (!exists)
            throw new NotFoundException(UserMessages.OperationClaimNotFound);
    }
    public async Task CheckIfUserAlreadyHasOperationClaim(int userId, int operationClaimId)
    {
        bool exists = await _userOperationClaimRepository.AnyAsync(uoc =>
            uoc.UserId == userId && uoc.OperationClaimId == operationClaimId);

        if (exists)
            throw new BusinessException(UserMessages.UserAlreadyHasOperationClaim);
    }

}
