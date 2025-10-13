using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommand:IRequest<DeletedUserResponse>
{
    public int Id { get; set; }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public DeleteUserCommandHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<DeletedUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.CheckIfIdExists(request.Id);

            User? user = await _userRepository.GetAsync(predicate: u => u.Id == request.Id,cancellationToken: cancellationToken);

            await _userRepository.DeleteAsync(user);

            return new DeletedUserResponse
            {
                Id = user.Id,
                Username = user.Username
            };
        }
    }
}
