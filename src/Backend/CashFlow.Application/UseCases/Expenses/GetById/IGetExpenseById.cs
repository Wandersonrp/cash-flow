using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public interface IGetExpenseById
{
    Task<ResponseExpenseJson> Execute(int id);
}
