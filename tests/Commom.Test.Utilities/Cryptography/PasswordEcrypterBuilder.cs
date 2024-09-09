using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace Commom.Test.Utilities.Cryptography;
public class PasswordEcrypterBuilder
{
    private readonly Mock<IPasswordEncrypter> _encrypter;
    private readonly Mock<IPasswordComparer> _comparer;

    public PasswordEcrypterBuilder()
    {
        _comparer = new Mock<IPasswordComparer>();

        _encrypter = new Mock<IPasswordEncrypter>();

        _encrypter.Setup(passwordEncrypter => passwordEncrypter.Encrypt(It.IsAny<string>())).Returns("hashed_password");
    }    

    public PasswordEcrypterBuilder Comparer(string password)
    {
        _comparer.Setup(comparer => comparer.Comparer(password, It.IsAny<string>())).Returns(true);

        return this;
    }

    public IPasswordComparer BuildComparer()
    {
        return _comparer.Object;
    }

    public IPasswordEncrypter BuildEncrypter()
    {
        return _encrypter.Object;
    }
}
