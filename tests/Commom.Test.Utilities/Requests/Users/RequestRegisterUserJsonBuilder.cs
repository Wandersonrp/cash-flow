using Bogus;
using CashFlow.Communication.Requests.Users;

namespace Commom.Test.Utilities.Requests.Users;
public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(r => r.Name, f => f.Person.FirstName)
            .RuleFor(r => r.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(r => r.Password, f => f.Internet.Password(prefix: "!Aa1"));
    }
}
