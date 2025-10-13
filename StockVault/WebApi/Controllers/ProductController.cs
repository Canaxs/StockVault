using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetByName;
using Application.Features.Products.Queries.GetList;
using Application.Features.Products.Queries.GetListCustomer;
using Application.Features.Products.Queries.GetListShipment;
using Application.Features.Products.Queries.GetListShipmentSummary;
using Application.Features.Products.Queries.GetListTopSellingProduct;
using Application.Features.Products.Queries.GetListWarehouse;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductCommand createProductCommand)
        {
            CreatedProductResponse response = await Mediator.Send(createProductCommand);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand updateProductCommand)
        {
            UpdatedProductResponse response = await Mediator.Send(updateProductCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedProductResponse response = await Mediator.Send(new DeleteProductCommand { Id = id });
            return Ok(response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdProductQuery getByIdProductQuery = new() { Id = id };
            GetByIdProductResponse response = await Mediator.Send(getByIdProductQuery);
            return Ok(response);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            GetByNameProductQuery getByNameProductQuery = new() { Name = name };
            GetByNameProductResponse response = await Mediator.Send(getByNameProductQuery);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProductQuery getListProductQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListProductListItemDto> response = await Mediator.Send(getListProductQuery);
            return Ok(response);
        }

        [HttpGet("{id}/Warehouse")]
        public async Task<IActionResult> GetListWarehouse([FromRoute] int id, [FromQuery] PageRequest pageRequest)
        {
            GetListWarehouseByProductIdQuery getListWarehouseByProductIdQuery = new() { PageRequest = pageRequest, Id = id };
            GetListResponse<GetListWarehouseByProductIdListItemDto> response = await Mediator.Send(getListWarehouseByProductIdQuery);
            return Ok(response);
        }

        [HttpGet("{id}/Shipments")]
        public async Task<IActionResult> GetListShipment([FromRoute] int id, [FromQuery] PageRequest pageRequest,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            GetListShipmentByProductIdQuery getListShipmentByProductIdQuery = new() { PageRequest = pageRequest, Id = id, StartDate = startDate, EndDate = endDate };
            GetListResponse<GetListShipmentByProductIdListItemDto> response = await Mediator.Send(getListShipmentByProductIdQuery);
            return Ok(response);
        }

        [HttpGet("{id}/Customers")]
        public async Task<IActionResult> GetListCustomers([FromRoute] int id, [FromQuery] PageRequest pageRequest,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            GetListCustomerByProductIdQuery getListCustomerByProductIdQuery = new() { PageRequest = pageRequest, Id = id, StartDate = startDate, EndDate = endDate };
            GetListResponse<GetListCustomerByProductIdListItemDto> response = await Mediator.Send(getListCustomerByProductIdQuery);
            return Ok(response);
        }

        [HttpGet("{id}/ShipmentSummary")]
        public async Task<IActionResult> GetShipmentSummary([FromRoute] int id,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            GetShipmentSummaryByProductIdQuery getShipmentSummaryByProductIdQuery = new() { Id = id, StartDate = startDate, EndDate = endDate };
            GetShipmentSummaryByProductIdResponse response = await Mediator.Send(getShipmentSummaryByProductIdQuery);
            return Ok(response);
        }

        [HttpGet("TopSellingProducts")]
        public async Task<IActionResult> GetListTopSellingProduct([FromQuery] PageRequest pageRequest, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            GetListTopSellingProductQuery getListTopSellingProductQuery = new() { PageRequest = pageRequest, StartDate = startDate, EndDate = endDate };
            GetListResponse<GetListTopSellingProductListItemDto> response = await Mediator.Send(getListTopSellingProductQuery);
            return Ok(response);
        }

    }
}
