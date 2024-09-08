using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infrastructure.Security.Cryptography;
public class Encrypter : IPasswordEncrypter, IPasswordComparer
{
    public bool Comparer(string password, string hash)
    {
        var isEqual = BC.Verify(password, hash);

        return isEqual;
    }

    public string Encrypt(string password)
    {
        var passwordEncrypter =  BC.HashPassword(password);

        return passwordEncrypter;
    }
}
