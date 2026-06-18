using Application.Features.StockMovements.Commands.Create;
using Application.Features.StockMovements.Commands.Delete;
using Application.Features.StockMovements.Commands.Update;
using Application.Features.StockMovements.Queries.GetById;
using Application.Features.StockMovements.Queries.GetList;
using Application.Features.StockMovements.Queries.GetListByDynamic;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockMovementsController : BaseController
{
    [HttpPost("create")]
    public async Task<ActionResult<CreatedStockMovementResponse>> Create([FromBody] CreateStockMovementCommand command)
    {
        CreatedStockMovementResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("update")]
    public async Task<ActionResult<UpdatedStockMovementResponse>> Update([FromBody] UpdateStockMovementCommand command)
    {
        UpdatedStockMovementResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("delete")]
    public async Task<ActionResult<DeletedStockMovementResponse>> Delete([FromBody] DeleteStockMovementCommand command)
    {
        DeletedStockMovementResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdStockMovementResponse>> GetById([FromRoute] Guid id, [FromQuery] Guid companyId)
    {
        GetByIdStockMovementQuery query = new() { Id = id, CompanyId = companyId };
        GetByIdStockMovementResponse response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListStockMovementListItemDto>>> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId,
        [FromQuery] string? searchTerm,
        [FromQuery] string? movementType
    )
    {
        GetListStockMovementQuery query = new()
        {
            PageRequest = pageRequest,
            CompanyId = companyId,
            SearchTerm = searchTerm,
            MovementType = movementType
        };

        GetListResponse<GetListStockMovementListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("GetListByDynamic")]
    public async Task<IActionResult> GetListByDynamic(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId,
        [FromBody] DynamicQuery dynamic
    )
    {
        GetListByDynamicStockMovementQuery getListByDynamicStockMovementQuery = new()
        {
            PageRequest = pageRequest,
            Dynamic = dynamic,
            CompanyId = companyId
        };
        GetListResponse<GetListByDynamicStockMovementListItemDto> response = await Mediator.Send(getListByDynamicStockMovementQuery);
        return Ok(response);
    }
}
