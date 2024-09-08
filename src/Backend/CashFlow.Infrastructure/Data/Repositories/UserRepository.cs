using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Data.Repositories;
internal class UserRepository : IUserRepository
{
    private readonly CashFlowDbContext _context;

    public UserRepository(CashFlowDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context
            .Users
            .AddAsync(user);
    }

    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
        var exists = await _context
            .Users
            .AnyAsync(x => x.Email.Equals(email));

        return exists;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var user = await _context
            .Users
            .AsNoTracking()            
            .FirstOrDefaultAsync(x => x.Email.Equals(email));

        return user;
    }
}
