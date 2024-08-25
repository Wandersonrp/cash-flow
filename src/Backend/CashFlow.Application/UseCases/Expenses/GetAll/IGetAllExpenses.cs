using CashFlow.Communication.Requests.Pagination;
using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public interface IGetAllExpenses
{
    Task<List<ResponseRegisterExpenseJson>> Execute(RequestPaginationJson pagination);
}
