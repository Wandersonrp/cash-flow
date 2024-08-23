using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums.Expenses;
using CashFlow.Communication.Requests.Expenses;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterExpenseValidator();

        var request = new RequestRegisterExpenseJson
        {
            Title = "Laptop",
            Date = DateTime.UtcNow.AddDays(-1),
            Amount = 825,
            Description = "Laptop for work",
            PaymentType = PaymentType.CreditCard,
        };

        var result = validator.Validate(request);       

        Assert.True(result.IsValid);
    }
}
