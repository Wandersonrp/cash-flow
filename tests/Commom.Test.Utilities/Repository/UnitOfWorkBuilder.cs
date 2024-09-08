using CashFlow.Domain.Repositories.UnitOfWork;
using Moq;

namespace Commom.Test.Utilities.Repository;
public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();

        return mock.Object;
    }
}
