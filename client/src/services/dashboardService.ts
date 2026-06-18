import type { DashboardSummary } from '../types/models';
import { mapDashboardSummary } from './apiMappers';
import { getJson } from './httpClient';

export async function fetchDashboardSummary(companyId: string): Promise<DashboardSummary> {
  const raw = await getJson<Record<string, unknown>>('/Dashboard/summary', { companyId });
  return mapDashboardSummary(raw);
}
