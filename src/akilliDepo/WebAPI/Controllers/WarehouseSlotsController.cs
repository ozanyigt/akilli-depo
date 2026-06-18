using Application.Features.WarehouseSlots.Commands.Create;
using Application.Features.WarehouseSlots.Commands.Delete;
using Application.Features.WarehouseSlots.Commands.Update;
using Application.Features.WarehouseSlots.Queries.GetById;
using Application.Features.WarehouseSlots.Queries.GetList;
using Application.Features.WarehouseSlots.Queries.GetListByDynamic;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseSlotsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedWarehouseSlotResponse>> Add([FromBody] CreateWarehouseSlotCommand command)
    {
        CreatedWarehouseSlotResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id, companyId = response.CompanyId }, response);
    }

    [HttpPost("warehouseslot-update")]
    public async Task<ActionResult<UpdatedWarehouseSlotResponse>> Update([FromBody] UpdateWarehouseSlotCommand command)
    {
        UpdatedWarehouseSlotResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpPost("warehouseslot-delete{id}")]
    public async Task<ActionResult<DeletedWarehouseSlotResponse>> Delete([FromRoute] Guid id, [FromQuery] Guid companyId)
    {
        DeleteWarehouseSlotCommand command = new() { Id = id, CompanyId = companyId };

        DeletedWarehouseSlotResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdWarehouseSlotResponse>> GetById([FromRoute] Guid id, [FromQuery] Guid companyId)
    {
        GetByIdWarehouseSlotQuery query = new() { Id = id, CompanyId = companyId };

        GetByIdWarehouseSlotResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListWarehouseSlotListItemDto>>> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId,
        [FromQuery] Guid? warehouseId
    )
    {
        GetListWarehouseSlotQuery query = new()
        {
            PageRequest = pageRequest,
            CompanyId = companyId,
            WarehouseId = warehouseId
        };

        GetListResponse<GetListWarehouseSlotListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpPost("GetListByDynamic")]
    public async Task<IActionResult> GetListByDynamic(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId,
        [FromBody] DynamicQuery dynamic
    )
    {
        GetListByDynamicWarehouseSlotQuery getListByDynamicWarehouseSlotQuery = new()
        {
            PageRequest = pageRequest,
            Dynamic = dynamic,
            CompanyId = companyId
        };
        GetListResponse<GetListByDynamicWarehouseSlotListItemDto> response = await Mediator.Send(getListByDynamicWarehouseSlotQuery);
        return Ok(response);
    }
}
