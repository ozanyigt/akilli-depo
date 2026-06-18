using Application.Common.Rules;
using Application.Features.WarehouseSlots.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.WarehouseSlots.Queries.GetById;

public class GetByIdWarehouseSlotQuery : IRequest<GetByIdWarehouseSlotResponse>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }

    public class GetByIdWarehouseSlotQueryHandler : IRequestHandler<GetByIdWarehouseSlotQuery, GetByIdWarehouseSlotResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseSlotRepository _warehouseSlotRepository;
        private readonly WarehouseSlotBusinessRules _warehouseSlotBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetByIdWarehouseSlotQueryHandler(
            IMapper mapper,
            IWarehouseSlotRepository warehouseSlotRepository,
            WarehouseSlotBusinessRules warehouseSlotBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _warehouseSlotRepository = warehouseSlotRepository;
            _warehouseSlotBusinessRules = warehouseSlotBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetByIdWarehouseSlotResponse> Handle(GetByIdWarehouseSlotQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            WarehouseSlot? warehouseSlot = await _warehouseSlotRepository.GetAsync(
                predicate: ws => ws.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _warehouseSlotBusinessRules.WarehouseSlotShouldExistWhenSelected(warehouseSlot);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouseSlot!.CompanyId, request.CompanyId);

            GetByIdWarehouseSlotResponse response = _mapper.Map<GetByIdWarehouseSlotResponse>(warehouseSlot);
            return response;
        }
    }
}
