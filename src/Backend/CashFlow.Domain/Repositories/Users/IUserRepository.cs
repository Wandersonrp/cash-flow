using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Users;
public interface IUserRepository
{
    Task AddAsync(User user);
    Task<bool> ExistsActiveUserWithEmail(string email);
}
