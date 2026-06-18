using Application.Common.Rules;
using Application.Features.Warehouses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Warehouses.Commands.Update;

public class UpdateWarehouseCommand : IRequest<UpdatedWarehouseResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Location { get; set; }
    public required int Capacity { get; set; }
    public required bool IsActive { get; set; }
    public required Guid CompanyId { get; set; }

    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, UpdatedWarehouseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public UpdateWarehouseCommandHandler(
            IMapper mapper,
            IWarehouseRepository warehouseRepository,
            WarehouseBusinessRules warehouseBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _warehouseRepository = warehouseRepository;
            _warehouseBusinessRules = warehouseBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<UpdatedWarehouseResponse> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == request.Id, cancellationToken: cancellationToken);
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouse!.CompanyId, request.CompanyId);

            warehouse = _mapper.Map(request, warehouse);

            await _warehouseRepository.UpdateAsync(warehouse!);

            UpdatedWarehouseResponse response = _mapper.Map<UpdatedWarehouseResponse>(warehouse);
            return response;
        }
    }
}
