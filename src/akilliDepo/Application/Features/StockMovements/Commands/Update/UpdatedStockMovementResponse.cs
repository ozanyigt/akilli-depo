using NArchitecture.Core.Application.Responses;

namespace Application.Features.StockMovements.Commands.Update;

public class UpdatedStockMovementResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid WarehouseSlotId { get; set; }
    public string MovementType { get; set; }
    public int Quantity { get; set; }
    public string ReferenceNo { get; set; }
    public string Description { get; set; }
    public DateTime MovementDate { get; set; }
    public Guid CompanyId { get; set; }
}
