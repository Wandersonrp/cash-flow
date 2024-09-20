using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseRepository
{
    Task<Expense?> GetByIdAsync(int id, int userId);  
    Task<bool> ExistsAsync(int id);
    Task<Expense> AddAsync(Expense expense);
    Task<(List<Expense> expenses, int count)> GetAllAsync(int page, int itemsPerPage, int userId);
    Task<bool> DeleteAsync(int id, int userId);
    void Update(Expense expense);
    Task<Expense?> GetByIdWithTracking(int id, int userId);
    Task<List<Expense>> FilterByMonth(DateOnly date, int userId);
    Task<int> CountAsync(int userId);
    Task<decimal> SumTotalAsync(int userId); 
}
