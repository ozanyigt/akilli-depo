using Application.Features.Warehouses.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Warehouses.Rules;

public class WarehouseBusinessRules : BaseBusinessRules
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ILocalizationService _localizationService;

    public WarehouseBusinessRules(IWarehouseRepository warehouseRepository, ILocalizationService localizationService)
    {
        _warehouseRepository = warehouseRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, WarehousesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task WarehouseShouldExistWhenSelected(Warehouse? warehouse)
    {
        if (warehouse == null)
            await throwBusinessException(WarehousesBusinessMessages.WarehouseNotExists);
    }

    public async Task WarehouseIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Warehouse? warehouse = await _warehouseRepository.GetAsync(
            predicate: w => w.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await WarehouseShouldExistWhenSelected(warehouse);
    }
}