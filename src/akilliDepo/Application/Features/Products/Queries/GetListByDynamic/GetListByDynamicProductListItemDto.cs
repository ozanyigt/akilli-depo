using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Queries.GetListByDynamic;

public class GetListByDynamicProductListItemDto : IDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
    public Guid CompanyId { get; set; }
    public decimal MinStockLevel { get; set; }
    public bool IsActive { get; set; }
}
