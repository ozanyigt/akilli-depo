using Application.Features.Warehouses.Commands.Create;
using Application.Features.Warehouses.Commands.Delete;
using Application.Features.Warehouses.Commands.Update;
using Application.Features.Warehouses.Queries.GetById;
using Application.Features.Warehouses.Queries.GetList;
using Application.Features.Warehouses.Queries.GetListByDynamic;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehousesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedWarehouseResponse>> Add([FromBody] CreateWarehouseCommand command)
    {
        CreatedWarehouseResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id, companyId = response.CompanyId }, response);
    }

    [HttpPost("warehouses-update")]
    public async Task<ActionResult<UpdatedWarehouseResponse>> Update([FromBody] UpdateWarehouseCommand command)
    {
        UpdatedWarehouseResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpPost("warehouses-delete{id}")]
    public async Task<ActionResult<DeletedWarehouseResponse>> Delete([FromRoute] Guid id, [FromQuery] Guid companyId)
    {
        DeleteWarehouseCommand command = new() { Id = id, CompanyId = companyId };

        DeletedWarehouseResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdWarehouseResponse>> GetById([FromRoute] Guid id, [FromQuery] Guid companyId)
    {
        GetByIdWarehouseQuery query = new() { Id = id, CompanyId = companyId };

        GetByIdWarehouseResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListWarehouseListItemDto>>> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId
    )
    {
        GetListWarehouseQuery query = new() { PageRequest = pageRequest, CompanyId = companyId };

        GetListResponse<GetListWarehouseListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpPost("GetListByDynamic")]
    public async Task<IActionResult> GetListByDynamic(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId,
        [FromBody] DynamicQuery dynamic
    )
    {
        GetListByDynamicWarehouseQuery getListByDynamicWarehouseQuery = new()
        {
            PageRequest = pageRequest,
            Dynamic = dynamic,
            CompanyId = companyId
        };
        GetListResponse<GetListByDynamicWarehouseListItemDto> response = await Mediator.Send(getListByDynamicWarehouseQuery);
        return Ok(response);
    }
}
