using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Web.Shared;
public static class DependencyInjectionExtensions
{
    public static void AddRazorShared(this IServiceCollection services)
    {
        services.AddBlazoredLocalStorage();
    }
}
