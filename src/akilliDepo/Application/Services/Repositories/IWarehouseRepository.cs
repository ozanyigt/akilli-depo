using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IWarehouseRepository : IAsyncRepository<Warehouse, Guid>, IRepository<Warehouse, Guid>
{
}