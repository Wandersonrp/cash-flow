namespace CashFlow.Communication.Responses.Expenses;

public record ResponseRegisterExpenseJson
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
}
