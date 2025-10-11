using Application.Features.Warehouses.Commands.Create;
using Application.Features.Warehouses.Commands.Delete;
using Application.Features.Warehouses.Commands.Update;
using Application.Features.Warehouses.Queries.GetById;
using Application.Features.Warehouses.Queries.GetList;
using Application.Features.Warehouses.Queries.GetListProduct;
using Application.Features.Warehouses.Queries.GetListShipment;
using Application.Features.Warehouses.Queries.GetListShipmentSummary;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateWarehouseCommand createWarehouseCommand)
        {
            CreatedWarehouseResponse response = await Mediator.Send(createWarehouseCommand);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateWarehouseCommand updateWarehouseCommand)
        {
            UpdatedWarehouseResponse response = await Mediator.Send(updateWarehouseCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedWarehouseResponse response = await Mediator.Send(new DeleteWarehouseCommand { Id = id});
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdWarehouseQuery getByIdWarehouseQuery = new() { Id = id };
            GetByIdWarehouseResponse response = await Mediator.Send(getByIdWarehouseQuery);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListWarehouseQuery getListWarehouseQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListWarehouseListItemDto> response = await Mediator.Send(getListWarehouseQuery);
            return Ok(response);
        }

        [HttpGet("{id}/Products")]
        public async Task<IActionResult> GetListProduct([FromQuery] PageRequest pageRequest, [FromRoute] int id)
        {
            GetListProductByWarehouseIdQuery getListProductByWarehouseIdQuery = new() { PageRequest = pageRequest, Id = id };
            GetListResponse<GetListProductByWarehouseIdListItemDto> response = await Mediator.Send(getListProductByWarehouseIdQuery);
            return Ok(response);
        }

        [HttpGet("{id}/Shipments")]
        public async Task<IActionResult> GetListShipment([FromRoute] int id, [FromQuery] PageRequest pageRequest, 
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            GetListShipmentByWarehouseIdQuery getListShipmentByWarehouseIdQuery = new() { PageRequest = pageRequest, Id = id , StartDate = startDate, EndDate = endDate};
            GetListResponse<GetListShipmentByWarehouseIdListItemDto> response = await Mediator.Send(getListShipmentByWarehouseIdQuery);
            return Ok(response);
        }

        [HttpGet("{id}/ShipmentSummary")]
        public async Task<IActionResult> GetListShipmentSummary([FromRoute] int id, [FromQuery] PageRequest pageRequest,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            GetListShipmentSummaryByWarehouseIdQuery getListShipmentSummaryByWarehouseIdQuery = new() { PageRequest = pageRequest, Id = id, StartDate = startDate, EndDate = endDate };
            GetListResponse<GetListShipmentSummaryByWarehouseIdListItemDto> response = await Mediator.Send(getListShipmentSummaryByWarehouseIdQuery);
            return Ok(response);
        }
    }
}
