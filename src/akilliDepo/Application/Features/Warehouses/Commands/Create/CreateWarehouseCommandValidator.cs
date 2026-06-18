using FluentValidation;

namespace Application.Features.Warehouses.Commands.Create;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Location).NotEmpty();
        RuleFor(c => c.Capacity).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}