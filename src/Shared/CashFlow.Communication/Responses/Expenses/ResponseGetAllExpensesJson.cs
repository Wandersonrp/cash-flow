namespace CashFlow.Communication.Responses.Expenses;

public record ResponseGetAllExpensesJson
{
    public ICollection<ResponseExpenseJson> Expenses = new List<ResponseExpenseJson>();
}
