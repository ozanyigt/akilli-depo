using Application.Common.Rules;
using Application.Features.Warehouses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Warehouses.Queries.GetById;

public class GetByIdWarehouseQuery : IRequest<GetByIdWarehouseResponse>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }

    public class GetByIdWarehouseQueryHandler : IRequestHandler<GetByIdWarehouseQuery, GetByIdWarehouseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetByIdWarehouseQueryHandler(
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

        public async Task<GetByIdWarehouseResponse> Handle(GetByIdWarehouseQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == request.Id, cancellationToken: cancellationToken);
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouse!.CompanyId, request.CompanyId);

            GetByIdWarehouseResponse response = _mapper.Map<GetByIdWarehouseResponse>(warehouse);
            return response;
        }
    }
}
