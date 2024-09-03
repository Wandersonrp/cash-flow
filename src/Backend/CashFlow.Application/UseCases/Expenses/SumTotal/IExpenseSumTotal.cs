namespace CashFlow.Application.UseCases.Expenses.SumTotal;

public interface IExpenseSumTotal
{
    Task<decimal> Execute();
}
