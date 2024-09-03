using CashFlow.Communication.Responses.Pagination;

namespace CashFlow.Communication.Responses.Expenses;

public record ResponseGetAllExpensesJson
{
    public List<ResponseExpenseJson> Expenses {get; set; }= new();
 
    public ResponsePaginationJson Pagination { get; set; } = new();
}
