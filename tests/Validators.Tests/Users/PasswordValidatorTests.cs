using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Validators.Users;
using FluentAssertions;
using FluentValidation;

namespace Validators.Tests.Users;
public class PasswordValidatorTests
{
    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaaa1")]
    public void Error_Invalid_Password(string password)
    {
        var validator = new PasswordValidator<RequestRegisterUserJson>();               

        var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);

        result            
            .Should()
            .BeFalse();       
    }
}
