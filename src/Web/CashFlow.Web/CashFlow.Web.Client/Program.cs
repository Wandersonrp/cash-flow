using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var cashFlowApiBaseAddress = builder.Configuration["AppSettings:CashFlowApiUrl"] ?? throw new InvalidOperationException("Provide an api url");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(cashFlowApiBaseAddress) });

builder.Services.AddMudServices();

await builder.Build().RunAsync();
