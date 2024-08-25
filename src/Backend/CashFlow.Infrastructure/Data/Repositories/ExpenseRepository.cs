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

    public async Task<bool> ExistsAsync(int id)
    {
        var expenseExists = await _context
            .Expenses
            .AnyAsync(x => x.Id == id);

        return expenseExists;
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
}
