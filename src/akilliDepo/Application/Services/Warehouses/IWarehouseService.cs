using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Warehouses;

public interface IWarehouseService
{
    Task<Warehouse?> GetAsync(
        Expression<Func<Warehouse, bool>> predicate,
        Func<IQueryable<Warehouse>, IIncludableQueryable<Warehouse, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Warehouse>?> GetListAsync(
        Expression<Func<Warehouse, bool>>? predicate = null,
        Func<IQueryable<Warehouse>, IOrderedQueryable<Warehouse>>? orderBy = null,
        Func<IQueryable<Warehouse>, IIncludableQueryable<Warehouse, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Warehouse> AddAsync(Warehouse warehouse);
    Task<Warehouse> UpdateAsync(Warehouse warehouse);
    Task<Warehouse> DeleteAsync(Warehouse warehouse, bool permanent = false);
}
