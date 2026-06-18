using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.WarehouseSlots;

public interface IWarehouseSlotService
{
    Task<WarehouseSlot?> GetAsync(
        Expression<Func<WarehouseSlot, bool>> predicate,
        Func<IQueryable<WarehouseSlot>, IIncludableQueryable<WarehouseSlot, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<WarehouseSlot>?> GetListAsync(
        Expression<Func<WarehouseSlot, bool>>? predicate = null,
        Func<IQueryable<WarehouseSlot>, IOrderedQueryable<WarehouseSlot>>? orderBy = null,
        Func<IQueryable<WarehouseSlot>, IIncludableQueryable<WarehouseSlot, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<WarehouseSlot> AddAsync(WarehouseSlot warehouseSlot);
    Task<WarehouseSlot> UpdateAsync(WarehouseSlot warehouseSlot);
    Task<WarehouseSlot> DeleteAsync(WarehouseSlot warehouseSlot, bool permanent = false);
}
