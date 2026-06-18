using NArchitecture.Core.Application.Responses;

namespace Application.Features.Companies.Queries.GetById;

public class GetByIdCompanyResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; set; }
    public Guid CompanyId { get; set; }
}