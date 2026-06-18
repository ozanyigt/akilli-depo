using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WarehouseSlotRepository : EfRepositoryBase<WarehouseSlot, Guid, BaseDbContext>, IWarehouseSlotRepository
{
    public WarehouseSlotRepository(BaseDbContext context) : base(context)
    {
    }
}