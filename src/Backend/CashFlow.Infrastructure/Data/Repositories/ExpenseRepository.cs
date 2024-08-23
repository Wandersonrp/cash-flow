using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.Data.Repositories;

public class ExpenseRepository : IExpenseReadOnlyRepository
{
    public Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Expense?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
