using CashFlow.Communication.Enums.Expenses;

namespace CashFlow.Communication.Requests.Expenses;

public record RequestRegisterExpenseJson
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public PaymentType PaymentType { get; init; }
}
