using NArchitecture.Core.Application.Responses;

namespace Application.Features.WarehouseSlots.Commands.Create;

public class CreatedWarehouseSlotResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid WarehouseId { get; set; }
    public string Code { get; set; }
    public string Zone { get; set; }
    public int Capacity { get; set; }
    public int CurrentStock { get; set; }
    public bool IsActive { get; set; }
    public Guid CompanyId { get; set; }
}