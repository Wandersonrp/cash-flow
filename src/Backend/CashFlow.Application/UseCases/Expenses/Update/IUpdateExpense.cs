using CashFlow.Communication.Requests.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Update;
public interface IUpdateExpense
{
    Task Execute(int id, RequestExpenseJson request);
}
