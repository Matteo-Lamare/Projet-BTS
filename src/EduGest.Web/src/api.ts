export interface TokenResponse {
  accessToken: string;
  refreshToken: string;
}

export interface CurrentUser {
  id: string;
  userName: string;
  email: string | null;
  roles: string[];
}

/** Le proxy Vite redirige /api vers ASP.NET Core en développement. */
export async function apiRequest<T>(
  endpoint: string,
  options: RequestInit = {},
  accessToken?: string,
): Promise<T> {
  const headers = new Headers(options.headers);
  headers.set("Accept", "application/json");
  if (options.body) headers.set("Content-Type", "application/json");
  if (accessToken) headers.set("Authorization", `Bearer ${accessToken}`);

  let response: Response;
  try {
    response = await fetch(`/api${endpoint}`, { ...options, headers });
  } catch {
    throw new Error("API inaccessible. Vérifiez que le backend ASP.NET Core est démarré.");
  }
  if (!response.ok) {
    if (response.status === 401) throw new Error("Identifiants invalides ou session expirée.");
    throw new Error(`Erreur API (HTTP ${response.status}).`);
  }
  return (await response.json()) as T;
}
