using DTOs;
using Microsoft.JSInterop;
using API.Clients;

namespace BlazorApp.Services
{
    public class BlazorAuthService : IAuthService
    {
        private readonly IJSRuntime _js;
        private const string TOKEN_KEY = "auth_token";
        private const string USERNAME_KEY = "auth_username";
        private const string EXPIRATION_KEY = "auth_expiration";

        public event Action<bool>? AuthenticationStateChanged;

        public BlazorAuthService(IJSRuntime js)
        {
            _js = js;
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
                var username = await GetUsernameAsync();
                return string.IsNullOrEmpty(username)
                    ? null
                    : new UserDTO { UserName = username };
            }
            catch (JSException jsEx)
            {
                throw new Exception($"Error obteniendo el usuario: {jsEx.Message}");
            }

        }

        //public async Task<bool> LoginAsync(string username, string password)
        //{
        //    var client = new AuthApiClient();
        //    var result = await client.LoginAsync(new LoginRequest
        //    {
        //        Username = username,
        //        Password = password
        //    });

        //    if (result == null) return false;

        //    await _js.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, result.Token);
        //    await _js.InvokeVoidAsync("localStorage.setItem", USERNAME_KEY, result.User.UserName);
        //    await _js.InvokeVoidAsync("localStorage.setItem", EXPIRATION_KEY, result.ExpiresAt.ToString("O"));

        //    AuthenticationStateChanged?.Invoke(true);
        //    return true;
        //}
        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var client = new AuthApiClient();
                var result = await client.LoginAsync(new LoginRequest
                {
                    Username = username,
                    Password = password
                });

                if (result == null)
                    return false;

                await _js.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, result.Token);
                await _js.InvokeVoidAsync("localStorage.setItem", USERNAME_KEY, result.User.UserName);
                await _js.InvokeVoidAsync("localStorage.setItem", EXPIRATION_KEY, result.ExpiresAt.ToString("O"));

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
