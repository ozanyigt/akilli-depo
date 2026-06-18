using FluentValidation;

namespace Application.Features.WarehouseSlots.Commands.Delete;

public class DeleteWarehouseSlotCommandValidator : AbstractValidator<DeleteWarehouseSlotCommand>
{
    public DeleteWarehouseSlotCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}