using Microsoft.Extensions.Configuration;

namespace CashFlow.Infrastructure.Extensions;
public static class ConfigurationExtensions
{
    public static bool IsTestEnvironment(this IConfiguration configuration)
    {
        var inMemoryTest = configuration.GetValue<bool>("InMemoryTest");

        return inMemoryTest;
    }
}
