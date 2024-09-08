using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infrastructure.Security.Cryptography;
public class Encrypter : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        var passwordEncrypter =  BC.HashPassword(password);

        return passwordEncrypter;
    }
}
