using Application.Common.Rules;
using Application.Features.Companies.Rules;
using Application.Features.Warehouses.Rules;
using Application.Features.WarehouseSlots.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.WarehouseSlots.Commands.Create;

public class CreateWarehouseSlotCommand : IRequest<CreatedWarehouseSlotResponse>, ILoggableRequest, ITransactionalRequest
{
    public required Guid WarehouseId { get; set; }
    public required string Code { get; set; }
    public required string Zone { get; set; }
    public required int Capacity { get; set; }
    public required int CurrentStock { get; set; }
    public required bool IsActive { get; set; }
    public required Guid CompanyId { get; set; }

    public class CreateWarehouseSlotCommandHandler : IRequestHandler<CreateWarehouseSlotCommand, CreatedWarehouseSlotResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseSlotRepository _warehouseSlotRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly WarehouseSlotBusinessRules _warehouseSlotBusinessRules;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly CompanyBusinessRules _companyBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public CreateWarehouseSlotCommandHandler(
            IMapper mapper,
            IWarehouseSlotRepository warehouseSlotRepository,
            IWarehouseRepository warehouseRepository,
            WarehouseSlotBusinessRules warehouseSlotBusinessRules,
            WarehouseBusinessRules warehouseBusinessRules,
            CompanyBusinessRules companyBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _warehouseSlotRepository = warehouseSlotRepository;
            _warehouseRepository = warehouseRepository;
            _warehouseSlotBusinessRules = warehouseSlotBusinessRules;
            _warehouseBusinessRules = warehouseBusinessRules;
            _companyBusinessRules = companyBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<CreatedWarehouseSlotResponse> Handle(CreateWarehouseSlotCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);
            await _companyBusinessRules.CompanyIdShouldExistWhenSelected(request.CompanyId, cancellationToken);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(
                predicate: w => w.Id == request.WarehouseId,
                cancellationToken: cancellationToken
            );
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouse!.CompanyId, request.CompanyId);

            WarehouseSlot warehouseSlot = _mapper.Map<WarehouseSlot>(request);

            await _warehouseSlotRepository.AddAsync(warehouseSlot);

            CreatedWarehouseSlotResponse response = _mapper.Map<CreatedWarehouseSlotResponse>(warehouseSlot);
            return response;
        }
    }
}
