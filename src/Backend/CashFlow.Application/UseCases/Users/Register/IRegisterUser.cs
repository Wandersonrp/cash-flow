using CashFlow.Communication.Requests.Users;

namespace CashFlow.Application.UseCases.Users.Register;
public interface IRegisterUser
{
    Task Execute(RequestRegisterUserJson request);
}
