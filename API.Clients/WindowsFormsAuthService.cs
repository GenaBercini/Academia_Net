using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
        public class WindowsFormsAuthService : IAuthService
        {
            private static string? _currentToken;
            private static DateTime _tokenExpiration;
            private static UserDTO _currentUser;

            public event Action<bool>? AuthenticationStateChanged;

            public async Task<bool> IsAuthenticatedAsync()
            {
                return !string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow < _tokenExpiration;
            }

            public async Task<string?> GetTokenAsync()
            {
                var isAuth = await IsAuthenticatedAsync();
                return isAuth ? _currentToken : null;
            }

        public async Task<UserDTO?> GetCurrentUserAsync()
        {
            var isAuth = await IsAuthenticatedAsync();
            return isAuth ? _currentUser : null;
        }


        public async Task<string?> GetUsernameAsync()
            {
                var isAuth = await IsAuthenticatedAsync();
                return isAuth ? _currentUser?.UserName : null;
            }

            public async Task<bool> LoginAsync(string username, string password)
            {
                var request = new LoginRequest
                {
                    Username = username,
                    Password = password
                };

                var authClient = new AuthApiClient();
                var response = await authClient.LoginAsync(request);

                if (response != null)
                {
                    _currentToken = response.Token;
                    _tokenExpiration = response.ExpiresAt;
                    _currentUser = response.User;

                    AuthenticationStateChanged?.Invoke(true);
                    return true;
                }

                return false;
            }

            public async Task LogoutAsync()
            {
                _currentToken = null;
                _tokenExpiration = default;
                _currentUser = null;

                AuthenticationStateChanged?.Invoke(false);
            }

            public async Task CheckTokenExpirationAsync()
            {
                if (!string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow >= _tokenExpiration)
                {
                    await LogoutAsync();
                }
            }
        }
}
