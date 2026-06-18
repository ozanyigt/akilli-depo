using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetList;
using Application.Features.Products.Queries.GetListByDynamic;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{
    [HttpPost("create")]
    public async Task<ActionResult<CreatedProductResponse>> Create([FromBody] CreateProductCommand command)
    {
        CreatedProductResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("update")]
    public async Task<ActionResult<UpdatedProductResponse>> Update([FromBody] UpdateProductCommand command)
    {
        UpdatedProductResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("delete")]
    public async Task<ActionResult<DeletedProductResponse>> Delete([FromBody] DeleteProductCommand command)
    {
        DeletedProductResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdProductResponse>> GetById([FromRoute] Guid id, [FromQuery] Guid companyId)
    {
        GetByIdProductQuery query = new() { Id = id, CompanyId = companyId };
        GetByIdProductResponse response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListProductListItemDto>>> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId,
        [FromQuery] string? searchTerm
    )
    {
        GetListProductQuery query = new()
        {
            PageRequest = pageRequest,
            CompanyId = companyId,
            SearchTerm = searchTerm
        };

        GetListResponse<GetListProductListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("GetListByDynamic")]
    public async Task<IActionResult> GetListByDynamic(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId,
        [FromBody] DynamicQuery dynamic
    )
    {
        GetListByDynamicProductQuery getListByDynamicProductQuery = new()
        {
            PageRequest = pageRequest,
            Dynamic = dynamic,
            CompanyId = companyId
        };
        GetListResponse<GetListByDynamicProductListItemDto> response = await Mediator.Send(getListByDynamicProductQuery);
        return Ok(response);
    }
}
