using Application.Common.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.StockMovements.Queries.GetList;

public class GetListStockMovementQuery : IRequest<GetListResponse<GetListStockMovementListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid CompanyId { get; set; }
    public string? SearchTerm { get; set; }
    public string? MovementType { get; set; }

    public class GetListStockMovementQueryHandler : IRequestHandler<GetListStockMovementQuery, GetListResponse<GetListStockMovementListItemDto>>
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListStockMovementQueryHandler(
            IStockMovementRepository stockMovementRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _stockMovementRepository = stockMovementRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListStockMovementListItemDto>> Handle(GetListStockMovementQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            string? search = string.IsNullOrWhiteSpace(request.SearchTerm) ? null : request.SearchTerm.Trim();
            string? movementType = string.IsNullOrWhiteSpace(request.MovementType)
                ? null
                : request.MovementType.Trim().ToUpperInvariant();

            IPaginate<StockMovement> stockMovements = await _stockMovementRepository.GetListAsync(
                predicate: sm =>
                    sm.CompanyId == request.CompanyId
                    && (movementType == null || sm.MovementType == movementType)
                    && (
                        search == null
                        || sm.ReferenceNo.Contains(search)
                        || sm.Description.Contains(search)
                        || sm.MovementType.Contains(search)
                    ),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStockMovementListItemDto> response = _mapper.Map<GetListResponse<GetListStockMovementListItemDto>>(stockMovements);
            return response;
        }
    }
}
