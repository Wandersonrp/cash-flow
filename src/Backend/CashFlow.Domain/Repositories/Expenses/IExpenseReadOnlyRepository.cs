using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseReadOnlyRepository
{
    Task<Expense?> GetByIdAsync(int id);  
    Task<bool> ExistsAsync(int id);  
}
