using NArchitecture.Core.Application.Responses;

namespace Application.Features.Warehouses.Commands.Delete;

public class DeletedWarehouseResponse : IResponse
{
    public Guid Id { get; set; }
}