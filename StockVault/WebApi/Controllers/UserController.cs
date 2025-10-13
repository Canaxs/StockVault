using Application.Features.Users.Command.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand )
        {
            CreatedUserResponse response = await Mediator.Send(createUserCommand);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            UpdatedUserResponse response = await Mediator.Send(updateUserCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedUserResponse response = await Mediator.Send(new DeleteUserCommand { Id = id });
            return Ok(response);
        }

    }
}
