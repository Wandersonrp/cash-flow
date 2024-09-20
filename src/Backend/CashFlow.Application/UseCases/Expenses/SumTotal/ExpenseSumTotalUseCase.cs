using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Expenses.SumTotal;
public class ExpenseSumTotalUseCase : IExpenseSumTotal
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ILoggedUser _loggedUser;

    public ExpenseSumTotalUseCase(IExpenseRepository expenseRepository, ILoggedUser loggedUser)
    {
        _expenseRepository = expenseRepository;
        _loggedUser = loggedUser;
    }

    public async Task<decimal> Execute()
    {
        var user = await _loggedUser.Get(); 

        return await _expenseRepository.SumTotalAsync(user.Id);
    }
}
