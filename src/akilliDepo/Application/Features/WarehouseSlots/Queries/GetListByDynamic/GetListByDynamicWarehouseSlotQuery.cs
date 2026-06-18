using Application.Common.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.WarehouseSlots.Queries.GetListByDynamic;

public class GetListByDynamicWarehouseSlotQuery : IRequest<GetListResponse<GetListByDynamicWarehouseSlotListItemDto>>, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }
    public Guid CompanyId { get; set; }

    public class GetListByDynamicWarehouseSlotQueryHandler : IRequestHandler<GetListByDynamicWarehouseSlotQuery, GetListResponse<GetListByDynamicWarehouseSlotListItemDto>>
    {
        private readonly IWarehouseSlotRepository _warehouseSlotRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListByDynamicWarehouseSlotQueryHandler(
            IWarehouseSlotRepository warehouseSlotRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _warehouseSlotRepository = warehouseSlotRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListByDynamicWarehouseSlotListItemDto>> Handle(
            GetListByDynamicWarehouseSlotQuery request,
            CancellationToken cancellationToken
        )
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            IPaginate<WarehouseSlot> warehouseSlots = await _warehouseSlotRepository.GetListByDynamicAsync(
                dynamic: request.Dynamic,
                predicate: ws => ws.CompanyId == request.CompanyId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByDynamicWarehouseSlotListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicWarehouseSlotListItemDto>>(warehouseSlots);
            return response;
        }
    }
}
