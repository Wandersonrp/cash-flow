using Bogus;
using CashFlow.Domain.Entities;
using Commom.Test.Utilities.Cryptography;

namespace Commom.Test.Utilities.Entities;
public class UserBuilder
{
    public static User Build()
    {
        var passwordEncripter = new PasswordEcrypterBuilder().BuildEncrypter();

        return new Faker<User>()
            .RuleFor(r => r.Id, _ => 1)
            .RuleFor(r => r.Name, f => f.Person.FirstName)
            .RuleFor(r => r.Email, (f, user) => f.Internet.Email(user.Email))
            .RuleFor(r => r.Password, (_, user) => passwordEncripter.Encrypt(user.Password))
            .RuleFor(r => r.UserIdentifier, _ => Guid.NewGuid());
    }
}
