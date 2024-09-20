using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.UnitOfWork;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpense
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteExpenseUseCase(IExpenseRepository expenseRepository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _expenseRepository = expenseRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(int id)
    {
        var user = await _loggedUser.Get();

        var result = await _expenseRepository
            .DeleteAsync(id, user.Id);

        if(result is false)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }
}
