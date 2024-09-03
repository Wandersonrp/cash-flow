using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Data.Repositories;

internal class ExpenseRepository : IExpenseRepository
{
    private readonly CashFlowDbContext _context;

    public ExpenseRepository(CashFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Expense> AddAsync(Expense expense)
    {
        var result = await _context
            .Expenses
            .AddAsync(expense);

        return result.Entity;
    }

    public async Task<int> CountAsync()
    {
        var count = await _context
            .Expenses
            .CountAsync();

        return count;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _context
            .Expenses
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
        {
            return false;
        }

        _context.Expenses.Remove(result);

        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        var expenseExists = await _context
            .Expenses
            .AnyAsync(x => x.Id == id);

        return expenseExists;
    }

    public async Task<List<Expense>> FilterByMonth(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1, 0, 0, 0, DateTimeKind.Utc).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);

        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59, DateTimeKind.Utc);

        return await _context
            .Expenses
            .AsNoTracking()
            .Where(x => x.Date >= startDate && x.Date <= endDate)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.Title)
            .ToListAsync();
    }

    public async Task<List<Expense>> GetAllAsync(int page, int itemsPerPage)
    {
        var expenses = await _context
            .Expenses
            .AsNoTracking()
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();

        return expenses;
    }

    public async Task<Expense?> GetByIdAsync(int id)
    {
        var expense = await _context
            .Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return expense;
    }

    public async Task<Expense?> GetByIdWithTracking(int id)
    {
        var expense = await _context
            .Expenses
            .FirstOrDefaultAsync(x => x.Id == id);

        return expense;
    }

    public async Task<decimal> SumTotalAsync()
    {
        return await _context
            .Expenses
            .SumAsync(x => x.Amount);
    }

    public void Update(Expense expense)
    {
        _context.Expenses.Update(expense);
    }
}
