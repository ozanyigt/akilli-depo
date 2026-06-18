using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StockMovements;

public interface IStockMovementService
{
    Task<StockMovement?> GetAsync(
        Expression<Func<StockMovement, bool>> predicate,
        Func<IQueryable<StockMovement>, IIncludableQueryable<StockMovement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StockMovement>?> GetListAsync(
        Expression<Func<StockMovement, bool>>? predicate = null,
        Func<IQueryable<StockMovement>, IOrderedQueryable<StockMovement>>? orderBy = null,
        Func<IQueryable<StockMovement>, IIncludableQueryable<StockMovement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StockMovement> AddAsync(StockMovement stockMovement);
    Task<StockMovement> UpdateAsync(StockMovement stockMovement);
    Task<StockMovement> DeleteAsync(StockMovement stockMovement, bool permanent = false);
}
