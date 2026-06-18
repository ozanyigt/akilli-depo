using NArchitecture.Core.Application.Dtos;

namespace Application.Features.WarehouseSlots.Queries.GetListByDynamic;

public class GetListByDynamicWarehouseSlotListItemDto : IDto
{
    public Guid WarehouseId { get; set; }
    public string Code { get; set; }
    public string Zone { get; set; }
    public int Capacity { get; set; }
    public int CurrentStock { get; set; }
    public bool IsActive { get; set; }
    public Guid CompanyId { get; set; }
}
