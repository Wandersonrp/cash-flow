using CashFlow.Communication.Enums.Expenses;

namespace CashFlow.Communication.Requests.Expenses;

public record RequestExpenseJson
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public PaymentType PaymentType { get; set; }
    public int UserId { get; set; }
}
