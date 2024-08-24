using CashFlow.Communication.Enums.Expenses;

namespace CashFlow.Communication.Requests.Expenses;

public record RequestRegisterExpenseJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public PaymentType PaymentType { get; set; }
}
