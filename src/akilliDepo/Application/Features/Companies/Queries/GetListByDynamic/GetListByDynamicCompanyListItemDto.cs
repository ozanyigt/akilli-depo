using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Companies.Queries.GetListByDynamic;

public class GetListByDynamicCompanyListItemDto : IDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; set; }
    public Guid CompanyId { get; set; }
}
