using Application.Common.Rules;
using Application.Features.Companies.Rules;
using Application.Features.Warehouses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Warehouses.Commands.Create;

public class CreateWarehouseCommand : IRequest<CreatedWarehouseResponse>, ILoggableRequest, ITransactionalRequest
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Location { get; set; }
    public required int Capacity { get; set; }
    public required bool IsActive { get; set; }
    public required Guid CompanyId { get; set; }

    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, CreatedWarehouseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly CompanyBusinessRules _companyBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public CreateWarehouseCommandHandler(
            IMapper mapper,
            IWarehouseRepository warehouseRepository,
            WarehouseBusinessRules warehouseBusinessRules,
            CompanyBusinessRules companyBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _warehouseRepository = warehouseRepository;
            _warehouseBusinessRules = warehouseBusinessRules;
            _companyBusinessRules = companyBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<CreatedWarehouseResponse> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);
            await _companyBusinessRules.CompanyIdShouldExistWhenSelected(request.CompanyId, cancellationToken);

            Warehouse warehouse = _mapper.Map<Warehouse>(request);

            await _warehouseRepository.AddAsync(warehouse);

            CreatedWarehouseResponse response = _mapper.Map<CreatedWarehouseResponse>(warehouse);
            return response;
        }
    }
}
