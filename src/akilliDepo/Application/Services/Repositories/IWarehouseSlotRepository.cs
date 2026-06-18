using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IWarehouseSlotRepository : IAsyncRepository<WarehouseSlot, Guid>, IRepository<WarehouseSlot, Guid>
{
}