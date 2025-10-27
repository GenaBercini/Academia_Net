using API.Clients;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using DTOs;

namespace BlazorApp.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _authService;

        public CustomAuthenticationStateProvider(IAuthService authService)
        {
            _authService = authService;
            _authService.AuthenticationStateChanged += OnAuthenticationStateChanged;
        }

        private void OnAuthenticationStateChanged(bool _) =>
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (!await _authService.IsAuthenticatedAsync())
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                var user = await _authService.GetCurrentUserAsync();
                if (user == null)
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim("name", $"{user.Name} {user.LastName}".Trim())
                };

                claims.Add(new Claim(ClaimTypes.Role, user.TypeUser.ToString()));

                var identity = new ClaimsIdentity(claims, "apiauth_type");
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}