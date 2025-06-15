using Ecomerce.frontend;
using Ecomerce.frontend.Auth;
using Ecomerce.frontend.Helpers;
using Ecomerce.frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
// inyectamos mud balzor
builder.Services.AddMudServices();
// inyectamos autorization core
builder.Services.AddAuthorizationCore();
// inyectamos el repository
builder.Services.AddScoped<IRepository, Repository>();
// servicios de auht
builder.Services.AddScoped<AuthenticationProviderJWT>();
// 
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UploadFiles>();
//
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
//
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

// capturar url para todos
var url = "https://localhost:7211";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });

await builder.Build().RunAsync();
