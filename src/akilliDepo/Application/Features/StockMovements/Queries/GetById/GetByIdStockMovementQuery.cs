using Application.Common.Rules;
using Application.Features.StockMovements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.StockMovements.Queries.GetById;

public class GetByIdStockMovementQuery : IRequest<GetByIdStockMovementResponse>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }

    public class GetByIdStockMovementQueryHandler : IRequestHandler<GetByIdStockMovementQuery, GetByIdStockMovementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly StockMovementBusinessRules _stockMovementBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetByIdStockMovementQueryHandler(
            IMapper mapper,
            IStockMovementRepository stockMovementRepository,
            StockMovementBusinessRules stockMovementBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _stockMovementRepository = stockMovementRepository;
            _stockMovementBusinessRules = stockMovementBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetByIdStockMovementResponse> Handle(GetByIdStockMovementQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            StockMovement? stockMovement = await _stockMovementRepository.GetAsync(
                predicate: sm => sm.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _stockMovementBusinessRules.StockMovementShouldExistWhenSelected(stockMovement);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(stockMovement!.CompanyId, request.CompanyId);

            GetByIdStockMovementResponse response = _mapper.Map<GetByIdStockMovementResponse>(stockMovement);
            return response;
        }
    }
}
