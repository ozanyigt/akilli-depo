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

namespace Application.Features.Warehouses.Queries.GetListByDynamic;

public class GetListByDynamicWarehouseQuery : IRequest<GetListResponse<GetListByDynamicWarehouseListItemDto>>, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }
    public Guid CompanyId { get; set; }

    public class GetListByDynamicWarehouseQueryHandler : IRequestHandler<GetListByDynamicWarehouseQuery, GetListResponse<GetListByDynamicWarehouseListItemDto>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListByDynamicWarehouseQueryHandler(
            IWarehouseRepository warehouseRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListByDynamicWarehouseListItemDto>> Handle(
            GetListByDynamicWarehouseQuery request,
            CancellationToken cancellationToken
        )
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            IPaginate<Warehouse> warehouses = await _warehouseRepository.GetListByDynamicAsync(
                dynamic: request.Dynamic,
                predicate: w => w.CompanyId == request.CompanyId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByDynamicWarehouseListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicWarehouseListItemDto>>(warehouses);
            return response;
        }
    }
}
