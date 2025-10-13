using Application.Features.Auth.Commands.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CreateTokenCommand createTokenCommand)
        {
            CreatedTokenResponse createdTokenResponse = await Mediator.Send(createTokenCommand);
            return Ok(createdTokenResponse);
        }
    }
}
