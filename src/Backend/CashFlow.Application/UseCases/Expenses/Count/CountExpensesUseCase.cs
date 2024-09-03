using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Count;
public class CountExpensesUseCase : ICountExpenses
{
    private readonly IExpenseRepository _expenseRepository;

    public CountExpensesUseCase(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<int> Execute()
    {
        return await _expenseRepository.CountAsync();
    }
}
