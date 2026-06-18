using Application.Features.Companies.Commands.Create;
using Application.Features.Companies.Commands.Delete;
using Application.Features.Companies.Commands.Update;
using Application.Features.Companies.Queries.GetById;
using Application.Features.Companies.Queries.GetList;
using Application.Features.Companies.Queries.GetListByDynamic;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedCompanyResponse>> Add([FromBody] CreateCompanyCommand command)
    {
        CreatedCompanyResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id, companyId = response.CompanyId }, response);
    }

    [HttpPost("comapnies-update")]
    public async Task<ActionResult<UpdatedCompanyResponse>> Update([FromBody] UpdateCompanyCommand command)
    {
        UpdatedCompanyResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpPost("comapnies-delete{id}")]
    public async Task<ActionResult<DeletedCompanyResponse>> Delete([FromRoute] Guid id, [FromQuery] Guid companyId)
    {
        DeleteCompanyCommand command = new() { Id = id, CompanyId = companyId };

        DeletedCompanyResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdCompanyResponse>> GetById([FromRoute] Guid id, [FromQuery] Guid companyId)
    {
        GetByIdCompanyQuery query = new() { Id = id, CompanyId = companyId };

        GetByIdCompanyResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListCompanyListItemDto>>> GetList(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId
    )
    {
        GetListCompanyQuery query = new() { PageRequest = pageRequest, CompanyId = companyId };

        GetListResponse<GetListCompanyListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpPost("GetListByDynamic")]
    public async Task<IActionResult> GetListByDynamic(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] Guid companyId,
        [FromBody] DynamicQuery dynamic
    )
    {
        GetListByDynamicCompanyQuery getListByDynamicCompanyQuery = new()
        {
            PageRequest = pageRequest,
            Dynamic = dynamic,
            CompanyId = companyId
        };
        GetListResponse<GetListByDynamicCompanyListItemDto> response = await Mediator.Send(getListByDynamicCompanyQuery);
        return Ok(response);
    }
}
