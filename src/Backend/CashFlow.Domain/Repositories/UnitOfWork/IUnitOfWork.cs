namespace CashFlow.Domain.Repositories.UnitOfWork;
public interface IUnitOfWork
{
    Task Commit();
}
