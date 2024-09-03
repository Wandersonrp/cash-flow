using System.Net.Http.Json;

namespace CashFlow.Web.Client.Services.Expenses;

public class ExpenseService : IExpenseService
{
    private readonly HttpClient _httpClient;

    public ExpenseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetTotalExpenses()
    {
      return await _httpClient.GetFromJsonAsync<int>("api/expenses/count");
    }
}
