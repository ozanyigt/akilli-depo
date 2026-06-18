import { DEFAULT_PAGE_SIZE } from '../config';
import type { PagedResponse, StockMovement, Warehouse, WarehouseSlot } from '../types/models';
import {
  mapPagedResponse,
  mapStockMovement,
  mapWarehouse,
  mapWarehouseSlot,
} from './apiMappers';
import { getJson, postJson } from './httpClient';

export async function fetchStockMovements(
  companyId: string,
  pageIndex: number,
  searchTerm?: string,
  movementType?: string
): Promise<PagedResponse<StockMovement>> {
  const raw = await getJson<Record<string, unknown>>('/StockMovements', {
    companyId,
    pageIndex,
    pageSize: DEFAULT_PAGE_SIZE,
    searchTerm,
    movementType,
  });
  const mapped = mapPagedResponse<Record<string, unknown>>(raw);
  return { ...mapped, items: mapped.items.map(mapStockMovement) };
}

export async function createStockMovement(
  companyId: string,
  payload: {
    productId: string;
    warehouseId: string;
    warehouseSlotId: string;
    movementType: string;
    quantity: number;
    referenceNo: string;
    description: string;
  }
) {
  return postJson('/StockMovements/create', {
    CompanyId: companyId,
    ProductId: payload.productId,
    WarehouseId: payload.warehouseId,
    WarehouseSlotId: payload.warehouseSlotId,
    MovementType: payload.movementType,
    Quantity: payload.quantity,
    ReferenceNo: payload.referenceNo,
    Description: payload.description,
    MovementDate: new Date().toISOString(),
  });
}

export async function fetchWarehouses(companyId: string): Promise<PagedResponse<Warehouse>> {
  const raw = await getJson<Record<string, unknown>>('/Warehouses', {
    companyId,
    pageIndex: 0,
    pageSize: 100,
  });
  const mapped = mapPagedResponse<Record<string, unknown>>(raw);
  return { ...mapped, items: mapped.items.map(mapWarehouse) };
}

export async function fetchWarehouseSlots(
  companyId: string,
  warehouseId?: string
): Promise<PagedResponse<WarehouseSlot>> {
  const raw = await getJson<Record<string, unknown>>('/WarehouseSlots', {
    companyId,
    pageIndex: 0,
    pageSize: 200,
    warehouseId,
  });
  const mapped = mapPagedResponse<Record<string, unknown>>(raw);
  return { ...mapped, items: mapped.items.map(mapWarehouseSlot) };
}
