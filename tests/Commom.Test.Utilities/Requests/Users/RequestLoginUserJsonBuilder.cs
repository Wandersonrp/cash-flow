using Bogus;
using CashFlow.Communication.Requests.Users;

namespace Commom.Test.Utilities.Requests.Users;
public class RequestLoginUserJsonBuilder
{
    public static RequestLoginUserJson Build()
    {
        return new Faker<RequestLoginUserJson>()
            .RuleFor(r => r.Email, f => f.Person.Email)
            .RuleFor(r => r.Password, f => f.Internet.Password(prefix: "!Aa1"));
    }
}
