using Application.Common.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Warehouses.Queries.GetList;

public class GetListWarehouseQuery : IRequest<GetListResponse<GetListWarehouseListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid CompanyId { get; set; }

    public class GetListWarehouseQueryHandler : IRequestHandler<GetListWarehouseQuery, GetListResponse<GetListWarehouseListItemDto>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListWarehouseQueryHandler(
            IWarehouseRepository warehouseRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListWarehouseListItemDto>> Handle(GetListWarehouseQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            IPaginate<Warehouse> warehouses = await _warehouseRepository.GetListAsync(
                predicate: w => w.CompanyId == request.CompanyId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWarehouseListItemDto> response = _mapper.Map<GetListResponse<GetListWarehouseListItemDto>>(warehouses);
            return response;
        }
    }
}
