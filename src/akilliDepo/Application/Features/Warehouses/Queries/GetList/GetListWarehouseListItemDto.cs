using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Warehouses.Queries.GetList;

public class GetListWarehouseListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public Guid CompanyId { get; set; }
}