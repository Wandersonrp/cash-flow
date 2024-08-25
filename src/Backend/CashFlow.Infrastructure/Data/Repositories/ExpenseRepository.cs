using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.Data.Context;

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
        var result = await _context.Expenses.AddAsync(expense);

        return result.Entity;
    }

    public Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Expense?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
