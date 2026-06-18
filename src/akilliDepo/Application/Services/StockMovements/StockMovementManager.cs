using Application.Features.StockMovements.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StockMovements;

public class StockMovementManager : IStockMovementService
{
    private readonly IStockMovementRepository _stockMovementRepository;
    private readonly StockMovementBusinessRules _stockMovementBusinessRules;

    public StockMovementManager(IStockMovementRepository stockMovementRepository, StockMovementBusinessRules stockMovementBusinessRules)
    {
        _stockMovementRepository = stockMovementRepository;
        _stockMovementBusinessRules = stockMovementBusinessRules;
    }

    public async Task<StockMovement?> GetAsync(
        Expression<Func<StockMovement, bool>> predicate,
        Func<IQueryable<StockMovement>, IIncludableQueryable<StockMovement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StockMovement? stockMovement = await _stockMovementRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return stockMovement;
    }

    public async Task<IPaginate<StockMovement>?> GetListAsync(
        Expression<Func<StockMovement, bool>>? predicate = null,
        Func<IQueryable<StockMovement>, IOrderedQueryable<StockMovement>>? orderBy = null,
        Func<IQueryable<StockMovement>, IIncludableQueryable<StockMovement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StockMovement> stockMovementList = await _stockMovementRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return stockMovementList;
    }

    public async Task<StockMovement> AddAsync(StockMovement stockMovement)
    {
        StockMovement addedStockMovement = await _stockMovementRepository.AddAsync(stockMovement);

        return addedStockMovement;
    }

    public async Task<StockMovement> UpdateAsync(StockMovement stockMovement)
    {
        StockMovement updatedStockMovement = await _stockMovementRepository.UpdateAsync(stockMovement);

        return updatedStockMovement;
    }

    public async Task<StockMovement> DeleteAsync(StockMovement stockMovement, bool permanent = false)
    {
        StockMovement deletedStockMovement = await _stockMovementRepository.DeleteAsync(stockMovement);

        return deletedStockMovement;
    }
}
