using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseRepository
{
    Task<Expense?> GetByIdAsync(int id);  
    Task<bool> ExistsAsync(int id);
    Task<Expense> AddAsync(Expense expense);
}
