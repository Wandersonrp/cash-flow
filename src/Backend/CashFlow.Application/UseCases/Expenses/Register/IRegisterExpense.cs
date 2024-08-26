using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public interface IRegisterExpense
{
    Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request);    
}
