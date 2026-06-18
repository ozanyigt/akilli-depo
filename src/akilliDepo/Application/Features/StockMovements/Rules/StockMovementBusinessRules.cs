using Application.Features.StockMovements.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.StockMovements.Rules;

public class StockMovementBusinessRules : BaseBusinessRules
{
    private readonly IStockMovementRepository _stockMovementRepository;
    private readonly ILocalizationService _localizationService;

    public StockMovementBusinessRules(IStockMovementRepository stockMovementRepository, ILocalizationService localizationService)
    {
        _stockMovementRepository = stockMovementRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, StockMovementsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task StockMovementShouldExistWhenSelected(StockMovement? stockMovement)
    {
        if (stockMovement == null)
            await throwBusinessException(StockMovementsBusinessMessages.StockMovementNotExists);
    }

    public async Task StockMovementIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StockMovement? stockMovement = await _stockMovementRepository.GetAsync(
            predicate: sm => sm.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StockMovementShouldExistWhenSelected(stockMovement);
    }

    public async Task MovementTypeMustBeValid(string movementType)
    {
        string normalized = movementType.Trim().ToUpperInvariant();
        if (normalized is not StockMovementTypes.In and not StockMovementTypes.Out)
            await throwBusinessException(StockMovementsBusinessMessages.InvalidMovementType);
    }

    public async Task QuantityMustBePositive(int quantity)
    {
        if (quantity <= 0)
            await throwBusinessException(StockMovementsBusinessMessages.QuantityMustBePositive);
    }

    public async Task WarehouseSlotMustBelongToWarehouse(WarehouseSlot warehouseSlot, Guid warehouseId)
    {
        if (warehouseSlot.WarehouseId != warehouseId)
            await throwBusinessException(StockMovementsBusinessMessages.WarehouseSlotMismatch);
    }

    public async Task StockMustBeAvailableForOut(WarehouseSlot warehouseSlot, int quantity)
    {
        if (warehouseSlot.CurrentStock < quantity)
            await throwBusinessException(StockMovementsBusinessMessages.InsufficientStock);
    }

    public async Task CapacityMustNotBeExceededForIn(WarehouseSlot warehouseSlot, int quantity)
    {
        if (warehouseSlot.CurrentStock + quantity > warehouseSlot.Capacity)
            await throwBusinessException(StockMovementsBusinessMessages.CapacityExceeded);
    }
}