using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace Commom.Test.Utilities.Repository;
public class ExpenseRepositoryBuilder
{
    private readonly Mock<IExpenseRepository> _repository;

    public ExpenseRepositoryBuilder()
    {
        _repository = new Mock<IExpenseRepository>();
    }

    public ExpenseRepositoryBuilder AddAsync(Expense expense)
    {
        _repository.Setup(repository => repository.AddAsync(It.IsAny<Expense>())).ReturnsAsync(expense);

        return this;
    }    

    public ExpenseRepositoryBuilder GetByIdAsync(Expense expense)
    {
        _repository.Setup(repository => repository.GetByIdAsync(expense.Id)).ReturnsAsync(expense);

        return this;
    }

    public IExpenseRepository Build()
    {
        return _repository.Object;
    }
}
