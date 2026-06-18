using Application.Features.StockMovements.Constants;
using Application.Common.Rules;
using Application.Features.Companies.Rules;
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

namespace Application.Features.StockMovements.Commands.Create;

public class CreateStockMovementCommand : IRequest<CreatedStockMovementResponse>, ILoggableRequest, ITransactionalRequest
{
    public required Guid ProductId { get; set; }
    public required Guid WarehouseId { get; set; }
    public required Guid WarehouseSlotId { get; set; }
    public required string MovementType { get; set; }
    public required int Quantity { get; set; }
    public required string ReferenceNo { get; set; }
    public required string Description { get; set; }
    public required DateTime MovementDate { get; set; }
    public required Guid CompanyId { get; set; }

    public class CreateStockMovementCommandHandler : IRequestHandler<CreateStockMovementCommand, CreatedStockMovementResponse>
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
        private readonly CompanyBusinessRules _companyBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public CreateStockMovementCommandHandler(
            IMapper mapper,
            IStockMovementRepository stockMovementRepository,
            IProductRepository productRepository,
            IWarehouseRepository warehouseRepository,
            IWarehouseSlotRepository warehouseSlotRepository,
            StockMovementBusinessRules stockMovementBusinessRules,
            ProductBusinessRules productBusinessRules,
            WarehouseBusinessRules warehouseBusinessRules,
            WarehouseSlotBusinessRules warehouseSlotBusinessRules,
            CompanyBusinessRules companyBusinessRules,
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
            _companyBusinessRules = companyBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<CreatedStockMovementResponse> Handle(CreateStockMovementCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);
            await _companyBusinessRules.CompanyIdShouldExistWhenSelected(request.CompanyId, cancellationToken);

            Product? product = await _productRepository.GetAsync(predicate: p => p.Id == request.ProductId, cancellationToken: cancellationToken);
            await _productBusinessRules.ProductShouldExistWhenSelected(product);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(product!.CompanyId, request.CompanyId);

            Warehouse? warehouse = await _warehouseRepository.GetAsync(predicate: w => w.Id == request.WarehouseId, cancellationToken: cancellationToken);
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouse!.CompanyId, request.CompanyId);

            WarehouseSlot? warehouseSlot = await _warehouseSlotRepository.GetAsync(
                predicate: ws => ws.Id == request.WarehouseSlotId,
                enableTracking: true,
                cancellationToken: cancellationToken
            );
            await _warehouseSlotBusinessRules.WarehouseSlotShouldExistWhenSelected(warehouseSlot);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(warehouseSlot!.CompanyId, request.CompanyId);
            await _stockMovementBusinessRules.WarehouseSlotMustBelongToWarehouse(warehouseSlot!, request.WarehouseId);
            await _stockMovementBusinessRules.MovementTypeMustBeValid(request.MovementType);
            await _stockMovementBusinessRules.QuantityMustBePositive(request.Quantity);

            string movementType = request.MovementType.Trim().ToUpperInvariant();
            if (movementType == StockMovementTypes.In)
                await _stockMovementBusinessRules.CapacityMustNotBeExceededForIn(warehouseSlot!, request.Quantity);
            else
                await _stockMovementBusinessRules.StockMustBeAvailableForOut(warehouseSlot!, request.Quantity);

            StockMovement stockMovement = _mapper.Map<StockMovement>(request);
            stockMovement.MovementType = movementType;

            if (movementType == StockMovementTypes.In)
                warehouseSlot!.CurrentStock += request.Quantity;
            else
                warehouseSlot!.CurrentStock -= request.Quantity;

            await _warehouseSlotRepository.UpdateAsync(warehouseSlot!);
            await _stockMovementRepository.AddAsync(stockMovement);

            CreatedStockMovementResponse response = _mapper.Map<CreatedStockMovementResponse>(stockMovement);
            return response;
        }
    }
}
