using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.UnitOfWork;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpense
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseUseCase(IExpenseRepository expenseRepository, IUnitOfWork unitOfWork)
    {
        _expenseRepository = expenseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(int id)
    {
        var result = await _expenseRepository
            .DeleteAsync(id);

        if(result is false)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }
}
