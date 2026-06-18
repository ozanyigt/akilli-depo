using NArchitecture.Core.Application.Responses;

namespace Application.Features.WarehouseSlots.Commands.Delete;

public class DeletedWarehouseSlotResponse : IResponse
{
    public Guid Id { get; set; }
}