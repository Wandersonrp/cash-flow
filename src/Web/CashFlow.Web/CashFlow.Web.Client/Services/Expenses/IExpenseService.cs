namespace CashFlow.Web.Client.Services.Expenses;

public interface IExpenseService
{
    Task<int> GetTotalExpenses();
}
