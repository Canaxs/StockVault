using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Application.Features.Customers.Queries.GetById;
using Application.Features.Customers.Queries.GetList;
using Application.Features.Customers.Queries.GetListProduct;
using Application.Features.Customers.Queries.GetListShipment;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            CreatedCustomerResponse response = await Mediator.Send(createCustomerCommand);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand updateCustomerCommand)
        {
            UpdatedCustomerResponse response = await Mediator.Send(updateCustomerCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedCustomerResponse response = await Mediator.Send(new DeleteCustomerCommand { Id = id });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdCustomerQuery getByIdCustomerQuery = new() { Id = id };
            GetByIdCustomerResponse response = await Mediator.Send(getByIdCustomerQuery);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCustomerQuery getListCustomerQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListCustomerListItemDto> response = await Mediator.Send(getListCustomerQuery);
            return Ok(response);
        }

        [HttpGet("{id}/Shipments")]
        public async Task<IActionResult> GetListShipment([FromRoute] int id, [FromQuery] PageRequest pageRequest,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            GetListShipmentByCustomerIdQuery getListShipmentByCustomerIdQuery = new() { Id = id,PageRequest = pageRequest,StartDate = startDate, EndDate = endDate };
            GetListResponse<GetListShipmentByCustomerIdListItemDto> response = await Mediator.Send(getListShipmentByCustomerIdQuery);
            return Ok(response);
        }

        [HttpGet("{id}/Products")]
        public async Task<IActionResult> GetListProduct([FromRoute] int id, [FromQuery] PageRequest pageRequest,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            GetListProductByCustomerIdQuery getListProductByCustomerIdQuery = new() { Id = id, PageRequest = pageRequest, StartDate = startDate, EndDate = endDate };
            GetListResponse<GetListProductByCustomerIdListItemDto> response = await Mediator.Send(getListProductByCustomerIdQuery);
            return Ok(response);
        }
    }
}
