namespace CashFlow.Application.UseCases.Expenses.Count;
public interface ICountExpenses
{
    Task<int> Execute();
}
