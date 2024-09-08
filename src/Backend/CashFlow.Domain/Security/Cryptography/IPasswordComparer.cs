namespace CashFlow.Domain.Security.Cryptography;
public interface IPasswordComparer
{
    bool Comparer(string password, string hash);
}
