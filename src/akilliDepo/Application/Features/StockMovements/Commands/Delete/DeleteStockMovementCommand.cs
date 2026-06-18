using Application.Common.Rules;
using Application.Features.StockMovements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.StockMovements.Commands.Delete;

public class DeleteStockMovementCommand : IRequest<DeletedStockMovementResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }

    public class DeleteStockMovementCommandHandler : IRequestHandler<DeleteStockMovementCommand, DeletedStockMovementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly StockMovementBusinessRules _stockMovementBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public DeleteStockMovementCommandHandler(
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

        public async Task<DeletedStockMovementResponse> Handle(DeleteStockMovementCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            StockMovement? stockMovement = await _stockMovementRepository.GetAsync(
                predicate: sm => sm.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _stockMovementBusinessRules.StockMovementShouldExistWhenSelected(stockMovement);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(stockMovement!.CompanyId, request.CompanyId);

            await _stockMovementRepository.DeleteAsync(stockMovement!);

            DeletedStockMovementResponse response = _mapper.Map<DeletedStockMovementResponse>(stockMovement);
            return response;
        }
    }
}
