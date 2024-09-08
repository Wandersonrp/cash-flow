using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace Commom.Test.Utilities.Cryptography;
public class PasswordEcrypterBuilder
{
    public static IPasswordEncrypter Build()
    {
        var mock = new Mock<IPasswordEncrypter>();

        mock.Setup(passwordEncrypter => passwordEncrypter.Encrypt(It.IsAny<string>())).Returns("hashed_password");

        return mock.Object;
    }
}
