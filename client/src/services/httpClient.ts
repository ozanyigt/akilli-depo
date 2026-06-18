import { API_BASE_URL } from '../config';

async function parseResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    const text = await response.text();
    if (text.includes('Invalid Hostname')) {
      throw new Error(
        'API hostname uyuşmazlığı. Visual Studio\'da WebAPI\'yi Shift+F5 ile durdurup F5 ile yeniden başlatın (http://localhost:5278/swagger).'
      );
    }
    if (text.trimStart().startsWith('<!DOCTYPE') || text.trimStart().startsWith('<HTML')) {
      throw new Error(`API hatası (HTTP ${response.status}). Sunucu beklenmeyen yanıt döndürdü.`);
    }
    throw new Error(text || `HTTP ${response.status}`);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return (await response.json()) as T;
}

export class ApiConnectionError extends Error {
  constructor(message: string) {
    super(message);
    this.name = 'ApiConnectionError';
  }
}

async function safeFetch(url: string, init?: RequestInit): Promise<Response> {
  try {
    return await fetch(url, init);
  } catch {
    throw new ApiConnectionError(
      'API\'ye bağlanılamadı. WebAPI çalışıyor mu? Visual Studio\'da WebAPI projesini F5 ile başlatın (http://localhost:5278).'
    );
  }
}

function buildQuery(params: Record<string, string | number | undefined | null>): string {
  const search = new URLSearchParams();
  Object.entries(params).forEach(([key, value]) => {
    if (value !== undefined && value !== null && value !== '') {
      search.set(key, String(value));
    }
  });
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function getJson<T>(
  path: string,
  params?: Record<string, string | number | undefined | null>
): Promise<T> {
  const url = `${API_BASE_URL}${path}${params ? buildQuery(params) : ''}`;
  const response = await safeFetch(url);
  return parseResponse<T>(response);
}

export async function postJson<T>(path: string, body: unknown): Promise<T> {
  const response = await safeFetch(`${API_BASE_URL}${path}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body),
  });
  return parseResponse<T>(response);
}
