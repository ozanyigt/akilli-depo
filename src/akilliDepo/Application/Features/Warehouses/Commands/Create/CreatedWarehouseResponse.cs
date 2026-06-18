using NArchitecture.Core.Application.Responses;

namespace Application.Features.Warehouses.Commands.Create;

public class CreatedWarehouseResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public Guid CompanyId { get; set; }
}