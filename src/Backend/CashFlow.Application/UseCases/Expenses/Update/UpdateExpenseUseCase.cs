using AutoMapper;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.UnitOfWork;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateExpenseUseCase : IUpdateExpense
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public UpdateExpenseUseCase(
        IExpenseRepository expenseRepository,
        IMapper mapper, 
        IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(int id, RequestExpenseJson request)
    {
        Validate(request);

        var user = await _loggedUser.Get();

        var expense = await _expenseRepository.GetByIdWithTracking(id, user.Id);

        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        _mapper.Map(request, expense);

        _expenseRepository.Update(expense);

        await _unitOfWork.Commit();
    }

    private static void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if(!result.IsValid)
        {
            var errors = result
                .Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}
