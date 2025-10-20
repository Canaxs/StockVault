using Application.Features.Auth.Commands.Create;
using Application.Features.Auth.Queries.GetByClaims;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("Claims")]
        public async Task<IActionResult> GetByClaims()
        {
            GetByClaimsResponse getByClaimsResponse = await Mediator.Send(new GetByClaimsQuery());
            return Ok(getByClaimsResponse);
        }
    }
}
