using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommand:IRequest<UpdatedUserResponse>
{
    public int Id { get; set; }
    public int OperationClaimId { get; set; }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules, IUserOperationClaimRepository userOperationClaimRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.CheckIfIdExists(request.Id);

            User? user = await _userRepository.GetAsync(predicate: u => u.Id == request.Id, cancellationToken: cancellationToken);

            await _userBusinessRules.CheckIfOperationClaimExists(request.OperationClaimId);
            await _userBusinessRules.CheckIfUserAlreadyHasOperationClaim(request.Id, request.OperationClaimId);

            UserOperationClaim userOperationClaim = new(user.Id, request.OperationClaimId);

            await _userOperationClaimRepository.AddAsync(userOperationClaim);

            User? newUser = await _userRepository.GetAsync(
                predicate: u => u.Id == request.Id, 
                include: q => q.Include(u => u.UserOperationClaims).ThenInclude(uoc => uoc.OperationClaim),
                cancellationToken: cancellationToken);

            return _mapper.Map<UpdatedUserResponse>(newUser);

        }
    }
}
