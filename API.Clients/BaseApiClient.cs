using System.Net;
using System.Net.Http.Headers;

namespace API.Clients
{
    public abstract class BaseApiClient
    {
        protected static async Task<HttpClient> CreateHttpClientAsync()
        {
            var client = new HttpClient();
            await ConfigureHttpClientAsync(client);
            return client;
        }

        protected static async Task ConfigureHttpClientAsync(HttpClient client)
        {
            string baseUrl = GetBaseUrlFromConfig();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            await AddAuthorizationHeaderAsync(client);
        }

        private static string GetBaseUrlFromConfig()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Intentando leer configuración...");

                string? envUrl = Environment.GetEnvironmentVariable("TPI_API_BASE_URL");
                if (!string.IsNullOrEmpty(envUrl))
                {
                    System.Diagnostics.Debug.WriteLine($"[DEBUG] URL desde variable de entorno: {envUrl}");
                    return envUrl;
                }

                string runtimeInfo = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier;
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Runtime: {runtimeInfo}");

                if (runtimeInfo.StartsWith("android"))
                {
                    System.Diagnostics.Debug.WriteLine($"[DEBUG] Detectado Android - usando IP de emulador");
                    return "http://10.0.2.2:5183/";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Error detectando plataforma: {ex.Message}");
            }

            string defaultUrl = "http://localhost:5183/";
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Usando URL por defecto: {defaultUrl}");
            return defaultUrl;
        }

        protected static async Task AddAuthorizationHeaderAsync(HttpClient client)
        {
            try
            {
                IAuthService authService;
                try
                {
                    authService = AuthServiceProvider.Instance;
                }
                catch (InvalidOperationException)
                {
                    return;
                }

                string? token = null;
                try
                {
                    token = await authService.GetTokenAsync();
                }
                catch
                {
                    token = null;
                }

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch
            {
            }
        }

        protected static async Task EnsureAuthenticatedAsync()
        {
            try
            {
                var authService = AuthServiceProvider.Instance;
                await authService.CheckTokenExpirationAsync();

                if (!await authService.IsAuthenticatedAsync())
                {
                    throw new UnauthorizedAccessException("Su sesión ha expirado.");
                }
            }
            catch (InvalidOperationException)
            {
                throw new UnauthorizedAccessException("Su sesión ha expirado.");
            }
        }

        protected static async Task HandleUnauthorizedResponseAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                try
                {
                    var authService = AuthServiceProvider.Instance;
                    await authService.LogoutAsync();
                }
                catch
                {
                }

                throw new UnauthorizedAccessException("Su sesión ha expirado.");
            }
        }
    }
}
