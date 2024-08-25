namespace CashFlow.Communication.Responses.Expenses;
public class ResponseExpenseJson
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
}
