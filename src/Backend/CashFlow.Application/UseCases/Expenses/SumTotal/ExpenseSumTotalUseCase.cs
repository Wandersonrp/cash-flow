
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.SumTotal;
public class ExpenseSumTotalUseCase : IExpenseSumTotal
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseSumTotalUseCase(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public Task<decimal> Execute()
    {
        return _expenseRepository.SumTotalAsync();
    }
}
