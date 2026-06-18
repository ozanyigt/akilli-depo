using Application.Features.Warehouses.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Warehouses;

public class WarehouseManager : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly WarehouseBusinessRules _warehouseBusinessRules;

    public WarehouseManager(IWarehouseRepository warehouseRepository, WarehouseBusinessRules warehouseBusinessRules)
    {
        _warehouseRepository = warehouseRepository;
        _warehouseBusinessRules = warehouseBusinessRules;
    }

    public async Task<Warehouse?> GetAsync(
        Expression<Func<Warehouse, bool>> predicate,
        Func<IQueryable<Warehouse>, IIncludableQueryable<Warehouse, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Warehouse? warehouse = await _warehouseRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return warehouse;
    }

    public async Task<IPaginate<Warehouse>?> GetListAsync(
        Expression<Func<Warehouse, bool>>? predicate = null,
        Func<IQueryable<Warehouse>, IOrderedQueryable<Warehouse>>? orderBy = null,
        Func<IQueryable<Warehouse>, IIncludableQueryable<Warehouse, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Warehouse> warehouseList = await _warehouseRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return warehouseList;
    }

    public async Task<Warehouse> AddAsync(Warehouse warehouse)
    {
        Warehouse addedWarehouse = await _warehouseRepository.AddAsync(warehouse);

        return addedWarehouse;
    }

    public async Task<Warehouse> UpdateAsync(Warehouse warehouse)
    {
        Warehouse updatedWarehouse = await _warehouseRepository.UpdateAsync(warehouse);

        return updatedWarehouse;
    }

    public async Task<Warehouse> DeleteAsync(Warehouse warehouse, bool permanent = false)
    {
        Warehouse deletedWarehouse = await _warehouseRepository.DeleteAsync(warehouse);

        return deletedWarehouse;
    }
}
