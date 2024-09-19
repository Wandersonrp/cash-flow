namespace CashFlow.Web.Client.Services.Token;

public interface ITokenProvider
{
    Task<string?> Get();
}
