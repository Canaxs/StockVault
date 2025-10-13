using Application.Features.Shipments.Commands.Create;
using Application.Features.Shipments.Commands.Delete;
using Application.Features.Shipments.Commands.Update;
using Application.Features.Shipments.Queries.GetById;
using Application.Features.Shipments.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShipmentController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateShipmentCommand createShipmentCommand)
        {
            CreatedShipmentResponse response = await Mediator.Send(createShipmentCommand);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateShipmentCommand updateShipmentCommand)
        {
            UpdatedShipmentResponse response = await Mediator.Send(updateShipmentCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedShipmentResponse response = await Mediator.Send(new DeleteShipmentCommand { Id = id });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdShipmentQuery getByIdShipmentQuery = new() { Id = id };
            GetByIdShipmentResponse response = await Mediator.Send(getByIdShipmentQuery);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListShipmentQuery getListShipmentQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListShipmentListItemDto> response = await Mediator.Send(getListShipmentQuery);
            return Ok(response);
        }

    }
}
