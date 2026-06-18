using Application.Features.WarehouseSlots.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.WarehouseSlots;

public class WarehouseSlotManager : IWarehouseSlotService
{
    private readonly IWarehouseSlotRepository _warehouseSlotRepository;
    private readonly WarehouseSlotBusinessRules _warehouseSlotBusinessRules;

    public WarehouseSlotManager(IWarehouseSlotRepository warehouseSlotRepository, WarehouseSlotBusinessRules warehouseSlotBusinessRules)
    {
        _warehouseSlotRepository = warehouseSlotRepository;
        _warehouseSlotBusinessRules = warehouseSlotBusinessRules;
    }

    public async Task<WarehouseSlot?> GetAsync(
        Expression<Func<WarehouseSlot, bool>> predicate,
        Func<IQueryable<WarehouseSlot>, IIncludableQueryable<WarehouseSlot, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        WarehouseSlot? warehouseSlot = await _warehouseSlotRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return warehouseSlot;
    }

    public async Task<IPaginate<WarehouseSlot>?> GetListAsync(
        Expression<Func<WarehouseSlot, bool>>? predicate = null,
        Func<IQueryable<WarehouseSlot>, IOrderedQueryable<WarehouseSlot>>? orderBy = null,
        Func<IQueryable<WarehouseSlot>, IIncludableQueryable<WarehouseSlot, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<WarehouseSlot> warehouseSlotList = await _warehouseSlotRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return warehouseSlotList;
    }

    public async Task<WarehouseSlot> AddAsync(WarehouseSlot warehouseSlot)
    {
        WarehouseSlot addedWarehouseSlot = await _warehouseSlotRepository.AddAsync(warehouseSlot);

        return addedWarehouseSlot;
    }

    public async Task<WarehouseSlot> UpdateAsync(WarehouseSlot warehouseSlot)
    {
        WarehouseSlot updatedWarehouseSlot = await _warehouseSlotRepository.UpdateAsync(warehouseSlot);

        return updatedWarehouseSlot;
    }

    public async Task<WarehouseSlot> DeleteAsync(WarehouseSlot warehouseSlot, bool permanent = false)
    {
        WarehouseSlot deletedWarehouseSlot = await _warehouseSlotRepository.DeleteAsync(warehouseSlot);

        return deletedWarehouseSlot;
    }
}
