using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStockMovementRepository : IAsyncRepository<StockMovement, Guid>, IRepository<StockMovement, Guid>
{
}