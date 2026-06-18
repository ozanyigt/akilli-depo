using FluentValidation;

namespace Application.Features.StockMovements.Commands.Delete;

public class DeleteStockMovementCommandValidator : AbstractValidator<DeleteStockMovementCommand>
{
    public DeleteStockMovementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}
