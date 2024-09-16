using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.UnitOfWork;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.Data.Context;
using CashFlow.Infrastructure.Data.Repositories;
using CashFlow.Infrastructure.Data.UnitOfWork;
using CashFlow.Infrastructure.Extensions;
using CashFlow.Infrastructure.Security.Cryptography;
using CashFlow.Infrastructure.Security.Tokens;
using CashFlow.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddUnitOfWork(services);
        AddSecurity(services, configuration);
        AddServices(services);

        if (!configuration.IsTestEnvironment())
        {
            AddDbContext(services, configuration);
        }
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services
            .AddScoped<IExpenseRepository, ExpenseRepository>()
            .AddScoped<IUserRepository, UserRepository>();
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

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddSecurity(IServiceCollection services, IConfiguration configuration)
    {
        var signingKey = configuration["AppSettings:Jwt:SigningKey"] ??
            throw new InvalidOperationException("Provide a JWT signing key");

        var expirationTimeMinutes = uint.Parse(configuration["AppSettings:Jwt:ExpirationTimeMinutes"] ??
            throw new InvalidOperationException("Provide a JWT expiration time in minutes"));

        services.AddScoped<IAccessTokenGenerator>(provider => new JwtTokenGenerator(signingKey, expirationTimeMinutes));
        services.AddScoped<IPasswordEncrypter, Encrypter>();
        services.AddScoped<IPasswordComparer, Encrypter>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }
}
