using Application.Common.Rules;
using Application.Features.Warehouses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Warehouses.Commands.Delete;

public class DeleteWarehouseCommand : IRequest<DeletedWarehouseResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }

    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, DeletedWarehouseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public DeleteWarehouseCommandHandler(
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

        public async Task<DeletedWarehouseResponse> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == request.Id, cancellationToken: cancellationToken);
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouse!.CompanyId, request.CompanyId);

            await _warehouseRepository.DeleteAsync(warehouse!);

            DeletedWarehouseResponse response = _mapper.Map<DeletedWarehouseResponse>(warehouse);
            return response;
        }
    }
}
