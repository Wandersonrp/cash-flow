using CashFlow.Communication.Enums.Expenses;

namespace CashFlow.Domain.Entities;

public class Expense
{
    public int Id { get; set; }    
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public PaymentType PaymentType { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = default!;
}
