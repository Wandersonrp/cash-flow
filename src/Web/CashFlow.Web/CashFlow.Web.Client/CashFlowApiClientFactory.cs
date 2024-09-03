namespace CashFlow.Web.Client;

public static class CashFlowApiClientFactory
{
    public static HttpClient CreateClient(IConfiguration configuration)
    {
        var cashFlowApiBaseAddress = configuration["AppSettings:CashFlowApiUrl"] ?? throw new InvalidOperationException("Provide an api url");       

        

        return new HttpClient { BaseAddress = new Uri(cashFlowApiBaseAddress) };
    }
}
