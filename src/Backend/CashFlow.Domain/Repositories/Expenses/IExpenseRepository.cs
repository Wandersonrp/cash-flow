using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseRepository
{
    Task<Expense?> GetByIdAsync(int id);  
    Task<bool> ExistsAsync(int id);
    Task<Expense> AddAsync(Expense expense);
    Task<List<Expense>> GetAllAsync(int page, int itemsPerPage);
    Task<bool> DeleteAsync(int id);
    void Update(Expense expense);
    Task<Expense?> GetByIdWithTracking(int id);
    Task<List<Expense>> FilterByMonth(DateOnly date);
    Task<int> CountAsync();
    Task<decimal> SumTotalAsync(); 
}
