using CashFlow.Web.Client.Services.Token;
using System.Net.Http.Headers;

namespace CashFlow.Web.Client;

public class CashFlowApiClientFactory
{    
    public async Task<HttpClient> CreateClient(IConfiguration configuration)
    {
        var cashFlowApiBaseAddress = configuration["AppSettings:CashFlowApiUrl"] ?? throw new InvalidOperationException("Provide an api url");                  
        return new HttpClient { BaseAddress = new Uri(cashFlowApiBaseAddress) };

        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenProvider.Get());        
    }
}
