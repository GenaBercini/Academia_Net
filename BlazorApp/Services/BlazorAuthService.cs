
using API.Clients;
using DTOs;
using Microsoft.JSInterop;
using System.Text.Json;
using DTOs;
using Microsoft.JSInterop;
using API.Clients;
using System.Text.Json;
using System.Linq;

namespace BlazorApp.Services
{
    public class BlazorAuthService : IAuthService
    {
        private readonly IJSRuntime _js;
        private readonly AuthApiClient _authClient;
        private const string TOKEN_KEY = "auth_token";
        private const string USERNAME_KEY = "auth_username";
        private const string EXPIRATION_KEY = "auth_expiration";
        private const string USER_KEY = "auth_user";

        public event Action<bool>? AuthenticationStateChanged;

        public BlazorAuthService(IJSRuntime js, AuthApiClient authClient)
        {
            _js = js;
            _authClient = authClient;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var token = await _js.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
                var expiration = await _js.InvokeAsync<string>("localStorage.getItem", EXPIRATION_KEY);

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(expiration))
                    return false;

                if (DateTime.TryParse(expiration, null, System.Globalization.DateTimeStyles.RoundtripKind, out var exp))
                {
                    return DateTime.UtcNow < exp;
                }
                return false;
            }
            catch (JSException jsEx)
            {
                throw new Exception($"Error leyendo token desde localStorage: {jsEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error verificando autenticación: {ex.Message}");
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await _js.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
            }
            catch (JSException jsEx)
            {
                throw new Exception($"Error leyendo el token de usuario: {jsEx.Message}");
            }
        }

        public async Task<string?> GetUsernameAsync()
        {
            try
            {
                return await _js.InvokeAsync<string>("localStorage.getItem", USERNAME_KEY);
            }
            catch (JSException jsEx)
            {
                throw new Exception($"Error leyendo el nombre de usuario: {jsEx.Message}");
            }
        }

        public async Task<UserDTO?> GetCurrentUserAsync()
        {
            try
            {
                var userJson = await _js.InvokeAsync<string>("localStorage.getItem", USER_KEY);
                if (!string.IsNullOrEmpty(userJson))
                {
                    var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var user = JsonSerializer.Deserialize<UserDTO>(userJson, opts);
                    if (user != null) return user;
                }

                // Fallback: si no hay user JSON, intentar obtener por username (si existe)
                var username = await GetUsernameAsync();
                if (string.IsNullOrEmpty(username)) return null;

                try
                {
                    var todos = (await UsersApiClient.GetAllAsync()).ToList();
                    var found = todos.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
                    if (found != null) return found;
                }
                catch
                {
                    // ignorar fallo en fallback
                }

                return new UserDTO { UserName = username };
            }
            catch (JSException jsEx)
            {
                throw new Exception($"Error obteniendo el usuario: {jsEx.Message}");
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var result = await _authClient.LoginAsync(new LoginRequest
                {
                    Username = username,
                    Password = password
                });

                if (result == null) return false;

                await _js.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, result.Token);
                await _js.InvokeVoidAsync("localStorage.setItem", USERNAME_KEY, result.User.UserName);
                await _js.InvokeVoidAsync("localStorage.setItem", EXPIRATION_KEY, result.ExpiresAt.ToString("O"));

                var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var userJson = JsonSerializer.Serialize(result.User, opts);
                await _js.InvokeVoidAsync("localStorage.setItem", USER_KEY, userJson);

                AuthenticationStateChanged?.Invoke(true);
                return true;
            }
            catch (JSException jsEx)
            {
                throw new Exception($"Error guardando datos en localStorage: {jsEx.Message}");
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Error comunicándose con la API: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en login: {ex.Message}");
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _js.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
                await _js.InvokeVoidAsync("localStorage.removeItem", USERNAME_KEY);
                await _js.InvokeVoidAsync("localStorage.removeItem", EXPIRATION_KEY);
                await _js.InvokeVoidAsync("localStorage.removeItem", USER_KEY);
                AuthenticationStateChanged?.Invoke(false);
            }
            catch (JSException jsEx)
            {
                throw new Exception($"Error deslogueando el usuario: {jsEx.Message}");
            }
        }

        public async Task CheckTokenExpirationAsync()
        {
            try
            {
                if (!await IsAuthenticatedAsync())
                    await LogoutAsync();
            }
            catch (JSException jsEx)
            {
                throw new Exception($"Error validando expiracion del Token el usuario: {jsEx.Message}");
            }
        }
    }
}
