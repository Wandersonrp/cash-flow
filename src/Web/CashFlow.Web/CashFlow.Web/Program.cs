using CashFlow.Web.Components;
using MudBlazor.Services;
using CashFlow.Web.Shared;
using CashFlow.Web.Client.Services.Token;
using CashFlow.Web.Client;
using CashFlow.Web;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var cashFlowApiBaseUrl = builder.Configuration.GetValue<string>("AppSettings:CashFlowApiUrl") ?? throw new InvalidOperationException("Provide a base api url");

builder.Services.AddHttpClient("CashFlow.Api", client =>
{
    client.BaseAddress = new Uri(cashFlowApiBaseUrl);
});

builder.Services.AddLocalization();

builder.Services.AddRazorShared();

builder.Services.AddScoped<ITokenProvider, TokenProvider>();

builder.Services.AddScoped<CashFlowApiClientFactory>();

builder.Services.AddScoped<LocalStorageAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures(new[] { "en", "pt-BR", "pt-PT" })
    .AddSupportedUICultures(new[] { "en", "pt-BR", "pt-PT" })
);

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CashFlow.Web.Client._Imports).Assembly);

app.Run();
