using Bogus;
using CashFlow.Communication.Enums.Expenses;
using CashFlow.Communication.Requests.Expenses;

namespace Commom.Test.Utilities.Requests.Expenses;

public class RequestRegisterExpenseJsonBuilder
{
    public RequestRegisterExpenseJson Build()
    {
        var faker = new Faker<RequestRegisterExpenseJson>()
            .RuleFor(r => r.Title, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Commerce.ProductDescription())
            .RuleFor(r => r.Date, f => f.Date.Past())
            .RuleFor(r => r.PaymentType, f => f.PickRandom<PaymentType>());

        return faker;
    }
}
