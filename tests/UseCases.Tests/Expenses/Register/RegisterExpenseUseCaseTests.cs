using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Domain.Entities;
using Commom.Test.Utilities.Entities;
using Commom.Test.Utilities.Mapper;
using Commom.Test.Utilities.Repository;
using Commom.Test.Utilities.Requests.Expenses;
using FluentAssertions;

namespace UseCases.Tests.Expenses.Register;
public class RegisterExpenseUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var expense = ExpenseBuilder.Build();        

        var useCase = CreateUseCase(expense);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Id.Should().Be(expense.Id);
    }

    private RegisterExpenseUseCase CreateUseCase(Expense expense)
    {
        var mapper = MapperBuilder.Build();
        var expenseRepository = new ExpenseRepositoryBuilder().AddAsync(expense).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new RegisterExpenseUseCase(expenseRepository, unitOfWork, mapper);
    }
}
