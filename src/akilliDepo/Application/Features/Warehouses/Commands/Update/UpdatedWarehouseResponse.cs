using NArchitecture.Core.Application.Responses;

namespace Application.Features.Warehouses.Commands.Update;

public class UpdatedWarehouseResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public Guid CompanyId { get; set; }
}