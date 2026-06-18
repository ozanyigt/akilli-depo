using NArchitecture.Core.Application.Responses;

namespace Application.Features.StockMovements.Commands.Delete;

public class DeletedStockMovementResponse : IResponse
{
    public Guid Id { get; set; }
}