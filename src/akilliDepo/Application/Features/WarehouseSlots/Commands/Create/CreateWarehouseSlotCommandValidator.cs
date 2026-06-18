using FluentValidation;

namespace Application.Features.WarehouseSlots.Commands.Create;

public class CreateWarehouseSlotCommandValidator : AbstractValidator<CreateWarehouseSlotCommand>
{
    public CreateWarehouseSlotCommandValidator()
    {
        RuleFor(c => c.WarehouseId).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Zone).NotEmpty();
        RuleFor(c => c.Capacity).NotEmpty();
        RuleFor(c => c.CurrentStock).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}
