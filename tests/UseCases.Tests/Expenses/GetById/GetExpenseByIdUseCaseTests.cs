using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Commom.Test.Utilities.Entities;
using Commom.Test.Utilities.Mapper;
using Commom.Test.Utilities.Repository;
using FluentAssertions;

namespace UseCases.Tests.Expenses.GetById;
public class GetExpenseByIdUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var expense = ExpenseBuilder.Build();
        
        var useCase = CreateUseCase(expense);

        var response = await useCase.Execute(expense.Id);

        response.Should().NotBeNull();
        response.Id.Should().Be(expense.Id);
    }

    [Fact]
    public async Task Expense_Not_Found()
    {
        var expense = ExpenseBuilder.Build();

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(expense.Id);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND));
    }

    private GetExpenseByIdUseCase CreateUseCase(Expense? expense = null)
    {
        var expenseRepository = new ExpenseRepositoryBuilder();
        var mapper = MapperBuilder.Build();

        if(expense is not null)
        {
            expenseRepository.GetByIdAsync(expense);
        }

        return new GetExpenseByIdUseCase(expenseRepository.Build(), mapper);
    }
}
