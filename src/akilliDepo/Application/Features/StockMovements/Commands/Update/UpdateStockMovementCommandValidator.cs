using FluentValidation;

namespace Application.Features.StockMovements.Commands.Update;

public class UpdateStockMovementCommandValidator : AbstractValidator<UpdateStockMovementCommand>
{
    public UpdateStockMovementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
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
