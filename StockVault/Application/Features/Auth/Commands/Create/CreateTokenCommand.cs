using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Create;

public class CreateTokenCommand : IRequest<CreatedTokenResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }

    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, CreatedTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly ITokenHelper _tokenHelper;

        public CreateTokenCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<CreatedTokenResponse> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.CheckIfUserExists(request.Username);

            User? user = await _userRepository.GetAsync(u => u.Username == request.Username,cancellationToken: cancellationToken);

            _authBusinessRules.VerifyPassword(user,request.Password);

            Paginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(
                predicate: u => u.UserId == user.Id,
                include: q => q.Include(u => u.OperationClaim),
                cancellationToken: cancellationToken
                );

            IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(u => new OperationClaim
            {
                Id = u.OperationClaim.Id,
                Name = u.OperationClaim.Name
            }).ToList();

            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);

            return new CreatedTokenResponse
            {
                Token = accessToken.Token
            };

        }
    }
}
