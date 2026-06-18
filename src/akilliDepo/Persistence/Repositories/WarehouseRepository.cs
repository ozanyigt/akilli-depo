using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WarehouseRepository : EfRepositoryBase<Warehouse, Guid, BaseDbContext>, IWarehouseRepository
{
    public WarehouseRepository(BaseDbContext context) : base(context)
    {
    }
}