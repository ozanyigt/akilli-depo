using Application.Features.Dashboard.Queries.GetSummary;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : BaseController
{
    [HttpGet("summary")]
    public async Task<ActionResult<DashboardSummaryDto>> GetSummary([FromQuery] Guid companyId)
    {
        GetDashboardSummaryQuery query = new() { CompanyId = companyId };
        DashboardSummaryDto response = await Mediator.Send(query);
        return Ok(response);
    }
}
