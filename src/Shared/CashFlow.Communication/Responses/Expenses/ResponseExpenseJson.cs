using CashFlow.Communication.Enums.Expenses;

namespace CashFlow.Communication.Responses.Expenses;
public class ResponseExpenseJson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public PaymentType PaymentType { get; set; }
}
