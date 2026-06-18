import { DEFAULT_PAGE_SIZE } from '../config';
import type { PagedResponse, Product } from '../types/models';
import { mapPagedResponse, mapProduct } from './apiMappers';
import { getJson, postJson } from './httpClient';

export async function fetchProducts(
  companyId: string,
  pageIndex: number,
  searchTerm?: string,
  pageSize: number = DEFAULT_PAGE_SIZE
): Promise<PagedResponse<Product>> {
  const raw = await getJson<Record<string, unknown>>('/Products', {
    companyId,
    pageIndex,
    pageSize,
    searchTerm,
  });
  const mapped = mapPagedResponse<Record<string, unknown>>(raw);
  return { ...mapped, items: mapped.items.map(mapProduct) };
}

export async function fetchAllProducts(companyId: string): Promise<Product[]> {
  const response = await fetchProducts(companyId, 0, undefined, 500);
  return response.items;
}

export async function createProduct(companyId: string, payload: Omit<Product, 'id' | 'companyId'>) {
  return postJson('/Products/create', {
    CompanyId: companyId,
    Name: payload.name,
    Code: payload.code,
    Unit: payload.unit,
    Description: payload.description,
    MinStockLevel: payload.minStockLevel,
    IsActive: payload.isActive,
  });
}

export async function updateProduct(companyId: string, payload: Product) {
  return postJson('/Products/update', {
    Id: payload.id,
    CompanyId: companyId,
    Name: payload.name,
    Code: payload.code,
    Unit: payload.unit,
    Description: payload.description,
    MinStockLevel: payload.minStockLevel,
    IsActive: payload.isActive,
  });
}

export async function deleteProduct(companyId: string, id: string) {
  return postJson('/Products/delete', { Id: id, CompanyId: companyId });
}
