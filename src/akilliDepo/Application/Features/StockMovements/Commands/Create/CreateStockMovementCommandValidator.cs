using FluentValidation;

namespace Application.Features.StockMovements.Commands.Create;

public class CreateStockMovementCommandValidator : AbstractValidator<CreateStockMovementCommand>
{
    public CreateStockMovementCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.WarehouseId).NotEmpty();
        RuleFor(c => c.WarehouseSlotId).NotEmpty();
        RuleFor(c => c.MovementType).NotEmpty();
        RuleFor(c => c.Quantity).NotEmpty();
        RuleFor(c => c.ReferenceNo).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.MovementDate).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}
