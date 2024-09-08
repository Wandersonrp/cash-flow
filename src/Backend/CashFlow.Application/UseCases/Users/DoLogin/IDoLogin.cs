using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.Users;

namespace CashFlow.Application.UseCases.Users.DoLogin;
public interface IDoLogin
{
    Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request);
}
