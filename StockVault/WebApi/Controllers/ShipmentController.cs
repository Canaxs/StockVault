using Application.Features.Products.Commands.Create;
using Application.Features.Products.Queries.GetById;
using Application.Features.Shipments.Commands.Create;
using Application.Features.Shipments.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateShipmentCommand createShipmentCommand)
        {
            CreatedShipmentResponse response = await Mediator.Send(createShipmentCommand);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdShipmentQuery getByIdShipmentQuery = new() { Id = id };
            GetByIdShipmentResponse response = await Mediator.Send(getByIdShipmentQuery);
            return Ok(response);
        }

    }
}
