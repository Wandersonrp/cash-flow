using CashFlow.Domain.Repositories.Users;
using Moq;

namespace Commom.Test.Utilities.Repository;
public class UserRepositoryBuilder
{
    private readonly Mock<IUserRepository> _repository;

    public UserRepositoryBuilder()
    {
        _repository = new Mock<IUserRepository>();
    }

    public void ExistsActiveUserWithEmail(string email)
    {
        _repository.Setup(repository => repository.ExistsActiveUserWithEmail(email)).ReturnsAsync(true);
    }

    public IUserRepository Build()
    {        
        return _repository.Object;
    }
}
