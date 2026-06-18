using FluentValidation;

namespace Application.Features.Warehouses.Commands.Update;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    public UpdateWarehouseCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Location).NotEmpty();
        RuleFor(c => c.Capacity).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}