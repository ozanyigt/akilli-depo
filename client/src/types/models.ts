export interface PagedResponse<T> {
  items: T[];
  count: number;
  index: number;
  size: number;
  pages: number;
}

export interface Product {
  id: string;
  name: string;
  code: string;
  unit: string;
  description: string;
  companyId: string;
  minStockLevel: number;
  isActive: boolean;
}

export interface StockMovement {
  id: string;
  productId: string;
  warehouseId: string;
  warehouseSlotId: string;
  movementType: string;
  quantity: number;
  referenceNo: string;
  description: string;
  movementDate: string;
  companyId: string;
}

export interface Warehouse {
  id: string;
  name: string;
  code: string;
  location: string;
  companyId: string;
  isActive: boolean;
}

export interface WarehouseSlot {
  id: string;
  warehouseId: string;
  code: string;
  zone: string;
  capacity: number;
  currentStock: number;
  isActive: boolean;
  companyId: string;
}

export interface DashboardSummary {
  totalProducts: number;
  totalWarehouses: number;
  totalStockMovements: number;
  totalCurrentStock: number;
  nearCapacitySlotCount: number;
  stockInCount: number;
  stockOutCount: number;
}

export interface ProductFormValues {
  id?: string;
  name: string;
  code: string;
  unit: string;
  description: string;
  minStockLevel: number;
  isActive: boolean;
}

export interface StockMovementFormValues {
  productId: string;
  warehouseId: string;
  warehouseSlotId: string;
  movementType: 'IN' | 'OUT';
  quantity: number;
  referenceNo: string;
  description: string;
}
