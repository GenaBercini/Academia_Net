using API.Clients;
using BlazorApp;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5183/")
});

builder.Services.AddScoped<IAuthService, BlazorAuthService>();
builder.Services.AddScoped<AuthApiClient>();

var host = builder.Build();

var authService = host.Services.GetRequiredService<IAuthService>();
API.Clients.AuthServiceProvider.Register(authService);

await host.RunAsync();
