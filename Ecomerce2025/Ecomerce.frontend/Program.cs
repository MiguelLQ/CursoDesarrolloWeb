using Ecomerce.frontend;
using Ecomerce.frontend.Helpers;
using Ecomerce.frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
// inyectamos mud balzor
builder.Services.AddMudServices();
// inyectamos el repository
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<UploadFiles>();
// capturar url para todos
var url = "https://localhost:7211";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });

await builder.Build().RunAsync();
