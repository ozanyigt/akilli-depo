using NArchitecture.Core.Application.Responses;

namespace Application.Features.Products.Commands.Update;

public class UpdatedProductResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
    public Guid CompanyId { get; set; }
    public decimal MinStockLevel { get; set; }
    public bool IsActive { get; set; }
}