using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpense
{
    public async Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        return new ResponseRegisterExpenseJson
        {
            Id = 1,
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount
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
