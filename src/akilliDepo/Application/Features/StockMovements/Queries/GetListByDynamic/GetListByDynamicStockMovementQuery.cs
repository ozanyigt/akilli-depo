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

namespace Application.Features.StockMovements.Queries.GetListByDynamic;

public class GetListByDynamicStockMovementQuery : IRequest<GetListResponse<GetListByDynamicStockMovementListItemDto>>, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }
    public Guid CompanyId { get; set; }

    public class GetListByDynamicStockMovementQueryHandler : IRequestHandler<GetListByDynamicStockMovementQuery, GetListResponse<GetListByDynamicStockMovementListItemDto>>
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListByDynamicStockMovementQueryHandler(
            IStockMovementRepository stockMovementRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _stockMovementRepository = stockMovementRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListByDynamicStockMovementListItemDto>> Handle(
            GetListByDynamicStockMovementQuery request,
            CancellationToken cancellationToken
        )
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            IPaginate<StockMovement> stockMovements = await _stockMovementRepository.GetListByDynamicAsync(
                dynamic: request.Dynamic,
                predicate: sm => sm.CompanyId == request.CompanyId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByDynamicStockMovementListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicStockMovementListItemDto>>(stockMovements);
            return response;
        }
    }
}
