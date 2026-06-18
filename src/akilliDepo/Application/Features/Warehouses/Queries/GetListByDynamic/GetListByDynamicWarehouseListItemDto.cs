using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Warehouses.Queries.GetListByDynamic;

public class GetListByDynamicWarehouseListItemDto : IDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public Guid CompanyId { get; set; }
}
