using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StockMovementRepository : EfRepositoryBase<StockMovement, Guid, BaseDbContext>, IStockMovementRepository
{
    public StockMovementRepository(BaseDbContext context) : base(context)
    {
    }
}