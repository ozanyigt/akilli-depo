import type { DashboardSummary, PagedResponse } from '../types/models';

function pick<T>(raw: Record<string, unknown>, camel: string, pascal: string): T {
  return (raw[camel] ?? raw[pascal]) as T;
}

export function mapPagedResponse<T>(raw: Record<string, unknown>): PagedResponse<T> {
  return {
    items: pick<T[]>(raw, 'items', 'Items') ?? [],
    count: Number(pick<number>(raw, 'count', 'Count') ?? 0),
    index: Number(pick<number>(raw, 'index', 'Index') ?? 0),
    size: Number(pick<number>(raw, 'size', 'Size') ?? 0),
    pages: Number(pick<number>(raw, 'pages', 'Pages') ?? 0),
  };
}

export function mapDashboardSummary(raw: Record<string, unknown>): DashboardSummary {
  return {
    totalProducts: Number(pick(raw, 'totalProducts', 'TotalProducts') ?? 0),
    totalWarehouses: Number(pick(raw, 'totalWarehouses', 'TotalWarehouses') ?? 0),
    totalStockMovements: Number(pick(raw, 'totalStockMovements', 'TotalStockMovements') ?? 0),
    totalCurrentStock: Number(pick(raw, 'totalCurrentStock', 'TotalCurrentStock') ?? 0),
    nearCapacitySlotCount: Number(pick(raw, 'nearCapacitySlotCount', 'NearCapacitySlotCount') ?? 0),
    stockInCount: Number(pick(raw, 'stockInCount', 'StockInCount') ?? 0),
    stockOutCount: Number(pick(raw, 'stockOutCount', 'StockOutCount') ?? 0),
  };
}

export function mapProduct(raw: Record<string, unknown>) {
  return {
    id: String(pick(raw, 'id', 'Id')),
    name: String(pick(raw, 'name', 'Name') ?? ''),
    code: String(pick(raw, 'code', 'Code') ?? ''),
    unit: String(pick(raw, 'unit', 'Unit') ?? ''),
    description: String(pick(raw, 'description', 'Description') ?? ''),
    companyId: String(pick(raw, 'companyId', 'CompanyId')),
    minStockLevel: Number(pick(raw, 'minStockLevel', 'MinStockLevel') ?? 0),
    isActive: Boolean(pick(raw, 'isActive', 'IsActive')),
  };
}

export function mapWarehouse(raw: Record<string, unknown>) {
  return {
    id: String(pick(raw, 'id', 'Id')),
    name: String(pick(raw, 'name', 'Name') ?? ''),
    code: String(pick(raw, 'code', 'Code') ?? ''),
    location: String(pick(raw, 'location', 'Location') ?? ''),
    companyId: String(pick(raw, 'companyId', 'CompanyId')),
    isActive: Boolean(pick(raw, 'isActive', 'IsActive')),
  };
}

export function mapWarehouseSlot(raw: Record<string, unknown>) {
  return {
    id: String(pick(raw, 'id', 'Id')),
    warehouseId: String(pick(raw, 'warehouseId', 'WarehouseId')),
    code: String(pick(raw, 'code', 'Code') ?? ''),
    zone: String(pick(raw, 'zone', 'Zone') ?? ''),
    capacity: Number(pick(raw, 'capacity', 'Capacity') ?? 0),
    currentStock: Number(pick(raw, 'currentStock', 'CurrentStock') ?? 0),
    isActive: Boolean(pick(raw, 'isActive', 'IsActive')),
    companyId: String(pick(raw, 'companyId', 'CompanyId')),
  };
}

export function mapStockMovement(raw: Record<string, unknown>) {
  return {
    id: String(pick(raw, 'id', 'Id')),
    productId: String(pick(raw, 'productId', 'ProductId')),
    warehouseId: String(pick(raw, 'warehouseId', 'WarehouseId')),
    warehouseSlotId: String(pick(raw, 'warehouseSlotId', 'WarehouseSlotId')),
    movementType: String(pick(raw, 'movementType', 'MovementType') ?? ''),
    quantity: Number(pick(raw, 'quantity', 'Quantity') ?? 0),
    referenceNo: String(pick(raw, 'referenceNo', 'ReferenceNo') ?? ''),
    description: String(pick(raw, 'description', 'Description') ?? ''),
    movementDate: String(pick(raw, 'movementDate', 'MovementDate') ?? ''),
    companyId: String(pick(raw, 'companyId', 'CompanyId')),
  };
}
