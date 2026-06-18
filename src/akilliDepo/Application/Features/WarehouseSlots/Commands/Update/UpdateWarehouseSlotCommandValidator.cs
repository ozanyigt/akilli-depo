using FluentValidation;

namespace Application.Features.WarehouseSlots.Commands.Update;

public class UpdateWarehouseSlotCommandValidator : AbstractValidator<UpdateWarehouseSlotCommand>
{
    public UpdateWarehouseSlotCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.WarehouseId).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Zone).NotEmpty();
        RuleFor(c => c.Capacity).NotEmpty();
        RuleFor(c => c.CurrentStock).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}