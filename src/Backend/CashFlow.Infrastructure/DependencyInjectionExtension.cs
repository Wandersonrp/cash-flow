using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.Data.Context;
using CashFlow.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services
            .AddScoped<IExpenseRepository, ExpenseRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("Provide a default connection string");

        services.AddDbContext<CashFlowDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
}
