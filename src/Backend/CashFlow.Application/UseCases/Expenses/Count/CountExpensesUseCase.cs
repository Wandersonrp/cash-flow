using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Expenses.Count;
public class CountExpensesUseCase : ICountExpenses
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ILoggedUser _loggedUser;

    public CountExpensesUseCase(IExpenseRepository expenseRepository, ILoggedUser loggedUser)
    {
        _expenseRepository = expenseRepository;
        _loggedUser = loggedUser;
    }

    public async Task<int> Execute()
    {
        var user = await _loggedUser.Get();

        return await _expenseRepository.CountAsync(user.Id);
    }
}
