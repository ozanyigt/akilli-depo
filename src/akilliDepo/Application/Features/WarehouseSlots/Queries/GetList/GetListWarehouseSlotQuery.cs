using Application.Common.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.WarehouseSlots.Queries.GetList;

public class GetListWarehouseSlotQuery : IRequest<GetListResponse<GetListWarehouseSlotListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? WarehouseId { get; set; }

    public class GetListWarehouseSlotQueryHandler : IRequestHandler<GetListWarehouseSlotQuery, GetListResponse<GetListWarehouseSlotListItemDto>>
    {
        private readonly IWarehouseSlotRepository _warehouseSlotRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListWarehouseSlotQueryHandler(
            IWarehouseSlotRepository warehouseSlotRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _warehouseSlotRepository = warehouseSlotRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListWarehouseSlotListItemDto>> Handle(GetListWarehouseSlotQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            IPaginate<WarehouseSlot> warehouseSlots = await _warehouseSlotRepository.GetListAsync(
                predicate: ws =>
                    ws.CompanyId == request.CompanyId
                    && (!request.WarehouseId.HasValue || ws.WarehouseId == request.WarehouseId.Value),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWarehouseSlotListItemDto> response = _mapper.Map<GetListResponse<GetListWarehouseSlotListItemDto>>(warehouseSlots);
            return response;
        }
    }
}
