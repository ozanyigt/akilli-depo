using FluentValidation;

namespace Application.Features.Warehouses.Commands.Delete;

public class DeleteWarehouseCommandValidator : AbstractValidator<DeleteWarehouseCommand>
{
    public DeleteWarehouseCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}