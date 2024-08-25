using CashFlow.Domain.Repositories.UnitOfWork;
using CashFlow.Infrastructure.Data.Context;

namespace CashFlow.Infrastructure.Data.UnitOfWork;
internal class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowDbContext _context;

    public UnitOfWork(CashFlowDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
