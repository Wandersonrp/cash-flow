using Bogus;
using CashFlow.Domain.Entities;

namespace Commom.Test.Utilities.Entities;
public class ExpenseBuilder
{
    public static Expense Build()
    {
        return new Faker<Expense>()
            .RuleFor(r => r.Id, _ => 1)
            .RuleFor(r => r.Title, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Commerce.ProductDescription())
            .RuleFor(r => r.Date, _ => DateTime.UtcNow)
            .RuleFor(r => r.Amount, f => decimal.Parse(f.Commerce.Price(decimals: 2)))
            .RuleFor(r => r.UserId, _ => 1);            
    }
}
