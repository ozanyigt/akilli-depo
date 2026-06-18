using Application.Features.WarehouseSlots.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.WarehouseSlots.Rules;

public class WarehouseSlotBusinessRules : BaseBusinessRules
{
    private readonly IWarehouseSlotRepository _warehouseSlotRepository;
    private readonly ILocalizationService _localizationService;

    public WarehouseSlotBusinessRules(IWarehouseSlotRepository warehouseSlotRepository, ILocalizationService localizationService)
    {
        _warehouseSlotRepository = warehouseSlotRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, WarehouseSlotsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task WarehouseSlotShouldExistWhenSelected(WarehouseSlot? warehouseSlot)
    {
        if (warehouseSlot == null)
            await throwBusinessException(WarehouseSlotsBusinessMessages.WarehouseSlotNotExists);
    }

    public async Task WarehouseSlotIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        WarehouseSlot? warehouseSlot = await _warehouseSlotRepository.GetAsync(
            predicate: ws => ws.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await WarehouseSlotShouldExistWhenSelected(warehouseSlot);
    }
}