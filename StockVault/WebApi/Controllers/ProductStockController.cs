using Application.Features.ProductStocks.Commands.Create;
using Application.Features.ProductStocks.Commands.Delete;
using Application.Features.ProductStocks.Commands.Update;
using Application.Features.ProductStocks.Queries.GetById;
using Application.Features.ProductStocks.Queries.GetList;
using Application.Features.ProductStocks.Queries.GetProductStockByProductAndWarehouse;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStockController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductStockCommand createProductStockCommand)
        {
            CreatedProductStockResponse response = await Mediator.Send(createProductStockCommand);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductStockCommand updateProductStockCommand)
        {
            UpdatedProductStockResponse response = await Mediator.Send(updateProductStockCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedProductStockResponse response = await Mediator.Send(new DeleteProductStockCommand { Id = id });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdProductStockQuery getByIdProductStockQuery = new() { Id = id };
            GetByIdProductStockResponse response = await Mediator.Send(getByIdProductStockQuery);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProductStockQuery getListProductStockQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListProductStockListItemDto> response = await Mediator.Send(getListProductStockQuery);
            return Ok(response);
        }

        [HttpGet("Product/{productId}/Warehouse/{warehouseId}")]
        public async Task<IActionResult> GetProductStockByProductAndWarehouse([FromRoute] int productId, [FromRoute] int warehouseId)
        {
            GetProductStockByProductIdAndWarehouseIdQuery getProductStockByProductIdAndWarehouseIdQuery = new() { ProductId = productId, WarehouseId = warehouseId};
            GetProductStockByProductIdAndWarehouseIdResponse response = await Mediator.Send(getProductStockByProductIdAndWarehouseIdQuery);
            return Ok(response);
        }

    }
}
