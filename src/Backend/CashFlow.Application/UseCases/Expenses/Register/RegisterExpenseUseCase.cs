using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpense
{
    private readonly IExpenseRepository _expenseRepository;

    public RegisterExpenseUseCase(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var expense = new Expense
        { 
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
            PaymentType = request.PaymentType,
            Date = request.Date
        };

        var createdExpense = await _expenseRepository.AddAsync(expense);

        return new ResponseRegisterExpenseJson
        {
            Id = createdExpense.Id,
            Title = createdExpense.Title,
            Description = createdExpense.Description,
            Date = createdExpense.Date,
            Amount = createdExpense.Amount            
        };
    }

    private static void Validate(RequestRegisterExpenseJson request)
    {   
        var validator = new RegisterExpenseValidator();

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
