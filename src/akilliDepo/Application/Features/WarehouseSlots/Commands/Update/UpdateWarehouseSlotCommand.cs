using Application.Common.Rules;
using Application.Features.Warehouses.Rules;
using Application.Features.WarehouseSlots.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.WarehouseSlots.Commands.Update;

public class UpdateWarehouseSlotCommand : IRequest<UpdatedWarehouseSlotResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid WarehouseId { get; set; }
    public required string Code { get; set; }
    public required string Zone { get; set; }
    public required int Capacity { get; set; }
    public required int CurrentStock { get; set; }
    public required bool IsActive { get; set; }
    public required Guid CompanyId { get; set; }

    public class UpdateWarehouseSlotCommandHandler : IRequestHandler<UpdateWarehouseSlotCommand, UpdatedWarehouseSlotResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseSlotRepository _warehouseSlotRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly WarehouseSlotBusinessRules _warehouseSlotBusinessRules;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public UpdateWarehouseSlotCommandHandler(
            IMapper mapper,
            IWarehouseSlotRepository warehouseSlotRepository,
            IWarehouseRepository warehouseRepository,
            WarehouseSlotBusinessRules warehouseSlotBusinessRules,
            WarehouseBusinessRules warehouseBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _warehouseSlotRepository = warehouseSlotRepository;
            _warehouseRepository = warehouseRepository;
            _warehouseSlotBusinessRules = warehouseSlotBusinessRules;
            _warehouseBusinessRules = warehouseBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<UpdatedWarehouseSlotResponse> Handle(UpdateWarehouseSlotCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            WarehouseSlot? warehouseSlot = await _warehouseSlotRepository.GetAsync(
                predicate: ws => ws.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _warehouseSlotBusinessRules.WarehouseSlotShouldExistWhenSelected(warehouseSlot);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouseSlot!.CompanyId, request.CompanyId);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(
                predicate: w => w.Id == request.WarehouseId,
                cancellationToken: cancellationToken
            );
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouse!.CompanyId, request.CompanyId);

            warehouseSlot = _mapper.Map(request, warehouseSlot);

            await _warehouseSlotRepository.UpdateAsync(warehouseSlot!);

            UpdatedWarehouseSlotResponse response = _mapper.Map<UpdatedWarehouseSlotResponse>(warehouseSlot);
            return response;
        }
    }
}
