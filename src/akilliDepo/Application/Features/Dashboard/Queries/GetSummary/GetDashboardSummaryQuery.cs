using Application.Common.Rules;
using Application.Services.Repositories;
using MediatR;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Dashboard.Queries.GetSummary;

public class GetDashboardSummaryQuery : IRequest<DashboardSummaryDto>
{
    public Guid CompanyId { get; set; }

    public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IWarehouseSlotRepository _warehouseSlotRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetDashboardSummaryQueryHandler(
            IProductRepository productRepository,
            IWarehouseRepository warehouseRepository,
            IWarehouseSlotRepository warehouseSlotRepository,
            IStockMovementRepository stockMovementRepository,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _warehouseSlotRepository = warehouseSlotRepository;
            _stockMovementRepository = stockMovementRepository;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<DashboardSummaryDto> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            IPaginate<Domain.Entities.Product> products = await _productRepository.GetListAsync(
                predicate: p => p.CompanyId == request.CompanyId,
                index: 0,
                size: 1,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            IPaginate<Domain.Entities.Warehouse> warehouses = await _warehouseRepository.GetListAsync(
                predicate: w => w.CompanyId == request.CompanyId,
                index: 0,
                size: 1,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            IPaginate<Domain.Entities.StockMovement> stockMovements = await _stockMovementRepository.GetListAsync(
                predicate: sm => sm.CompanyId == request.CompanyId,
                index: 0,
                size: 1,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            IPaginate<Domain.Entities.WarehouseSlot> slots = await _warehouseSlotRepository.GetListAsync(
                predicate: ws => ws.CompanyId == request.CompanyId,
                index: 0,
                size: 1000,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            int totalCurrentStock = slots.Items.Sum(s => s.CurrentStock);
            int nearCapacitySlots = slots.Items.Count(s => s.Capacity > 0 && s.CurrentStock >= s.Capacity * 0.9);

            IPaginate<Domain.Entities.StockMovement> stockInMovements = await _stockMovementRepository.GetListAsync(
                predicate: sm => sm.CompanyId == request.CompanyId && sm.MovementType == "IN",
                index: 0,
                size: 1,
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            IPaginate<Domain.Entities.StockMovement> stockOutMovements = await _stockMovementRepository.GetListAsync(
                predicate: sm => sm.CompanyId == request.CompanyId && sm.MovementType == "OUT",
                index: 0,
                size: 1,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            return new DashboardSummaryDto
            {
                TotalProducts = products.Count,
                TotalWarehouses = warehouses.Count,
                TotalStockMovements = stockMovements.Count,
                TotalCurrentStock = totalCurrentStock,
                NearCapacitySlotCount = nearCapacitySlots,
                StockInCount = stockInMovements.Count,
                StockOutCount = stockOutMovements.Count
            };
        }
    }
}

public class DashboardSummaryDto
{
    public int TotalProducts { get; set; }
    public int TotalWarehouses { get; set; }
    public int TotalStockMovements { get; set; }
    public int TotalCurrentStock { get; set; }
    public int NearCapacitySlotCount { get; set; }
    public int StockInCount { get; set; }
    public int StockOutCount { get; set; }
}
