using Application.Common.Rules;
using Application.Features.WarehouseSlots.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.WarehouseSlots.Commands.Delete;

public class DeleteWarehouseSlotCommand : IRequest<DeletedWarehouseSlotResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }

    public class DeleteWarehouseSlotCommandHandler : IRequestHandler<DeleteWarehouseSlotCommand, DeletedWarehouseSlotResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseSlotRepository _warehouseSlotRepository;
        private readonly WarehouseSlotBusinessRules _warehouseSlotBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public DeleteWarehouseSlotCommandHandler(
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

        public async Task<DeletedWarehouseSlotResponse> Handle(DeleteWarehouseSlotCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            WarehouseSlot? warehouseSlot = await _warehouseSlotRepository.GetAsync(
                predicate: ws => ws.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _warehouseSlotBusinessRules.WarehouseSlotShouldExistWhenSelected(warehouseSlot);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouseSlot!.CompanyId, request.CompanyId);

            await _warehouseSlotRepository.DeleteAsync(warehouseSlot!);

            DeletedWarehouseSlotResponse response = _mapper.Map<DeletedWarehouseSlotResponse>(warehouseSlot);
            return response;
        }
    }
}
