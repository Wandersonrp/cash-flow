using AutoMapper;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.UnitOfWork;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpense
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterExpenseUseCase(
        IExpenseRepository expenseRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper, ILoggedUser loggedUser)
    {
        _expenseRepository = expenseRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var expense = _mapper.Map<Expense>(request); 
        
        expense.UserId = loggedUser.Id;

        var createdExpense = await _expenseRepository.AddAsync(expense);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpenseJson>(createdExpense);
    }

    private static void Validate(RequestExpenseJson request)
    {   
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if(!result.IsValid)
        {
            var errorMessages = result
                .Errors
                .Select(x => x.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
