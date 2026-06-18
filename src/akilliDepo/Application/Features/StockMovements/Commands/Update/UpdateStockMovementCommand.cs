using Application.Common.Rules;
using Application.Features.Products.Rules;
using Application.Features.StockMovements.Rules;
using Application.Features.Warehouses.Rules;
using Application.Features.WarehouseSlots.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.StockMovements.Commands.Update;

public class UpdateStockMovementCommand : IRequest<UpdatedStockMovementResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid ProductId { get; set; }
    public required Guid WarehouseId { get; set; }
    public required Guid WarehouseSlotId { get; set; }
    public required string MovementType { get; set; }
    public required int Quantity { get; set; }
    public required string ReferenceNo { get; set; }
    public required string Description { get; set; }
    public required DateTime MovementDate { get; set; }
    public required Guid CompanyId { get; set; }

    public class UpdateStockMovementCommandHandler : IRequestHandler<UpdateStockMovementCommand, UpdatedStockMovementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IWarehouseSlotRepository _warehouseSlotRepository;
        private readonly StockMovementBusinessRules _stockMovementBusinessRules;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly WarehouseSlotBusinessRules _warehouseSlotBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public UpdateStockMovementCommandHandler(
            IMapper mapper,
            IStockMovementRepository stockMovementRepository,
            IProductRepository productRepository,
            IWarehouseRepository warehouseRepository,
            IWarehouseSlotRepository warehouseSlotRepository,
            StockMovementBusinessRules stockMovementBusinessRules,
            ProductBusinessRules productBusinessRules,
            WarehouseBusinessRules warehouseBusinessRules,
            WarehouseSlotBusinessRules warehouseSlotBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _stockMovementRepository = stockMovementRepository;
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _warehouseSlotRepository = warehouseSlotRepository;
            _stockMovementBusinessRules = stockMovementBusinessRules;
            _productBusinessRules = productBusinessRules;
            _warehouseBusinessRules = warehouseBusinessRules;
            _warehouseSlotBusinessRules = warehouseSlotBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<UpdatedStockMovementResponse> Handle(UpdateStockMovementCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            StockMovement? stockMovement = await _stockMovementRepository.GetAsync(
                predicate: sm => sm.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _stockMovementBusinessRules.StockMovementShouldExistWhenSelected(stockMovement);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(stockMovement!.CompanyId, request.CompanyId);

            Product? product = await _productRepository.GetAsync(predicate: p => p.Id == request.ProductId, cancellationToken: cancellationToken);
            await _productBusinessRules.ProductShouldExistWhenSelected(product);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(product!.CompanyId, request.CompanyId);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == request.WarehouseId, cancellationToken: cancellationToken);
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouse!.CompanyId, request.CompanyId);

            WarehouseSlot? warehouseSlot = await _warehouseSlotRepository.GetAsync(
                predicate: ws => ws.Id == request.WarehouseSlotId,
                cancellationToken: cancellationToken
            );
            await _warehouseSlotBusinessRules.WarehouseSlotShouldExistWhenSelected(warehouseSlot);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouseSlot!.CompanyId, request.CompanyId);

            stockMovement = _mapper.Map(request, stockMovement);

            await _stockMovementRepository.UpdateAsync(stockMovement!);

            UpdatedStockMovementResponse response = _mapper.Map<UpdatedStockMovementResponse>(stockMovement);
            return response;
        }
    }
}
