using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.Users;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Users.DoLogin;
public class DoLoginUseCase : IDoLogin
{
    private readonly IUserRepository _userRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordComparer _passwordComparer;
    
    public DoLoginUseCase(
        IUserRepository userRepository, 
        IAccessTokenGenerator accessTokenGenerator, 
        IPasswordComparer passwordComparer)
    {
        _userRepository = userRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordComparer = passwordComparer;
    }

    public async Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request)
    {
        var userExists = await _userRepository.GetByEmail(request.Email);

        if(userExists is null)
        {
            throw new InvalidCredentialsException(ResourceErrorMessages.INVALID_CREDENTIALS);
        }

        var passwordMatch = _passwordComparer.Comparer(request.Password, userExists.Password);

        if(!passwordMatch)
        {
            throw new InvalidCredentialsException(ResourceErrorMessages.INVALID_CREDENTIALS);
        }

        var token = _accessTokenGenerator.Generate(userExists);

        return new ResponseLoginUserJson
        {
            Name = userExists.Name,
            Token = token,
        };
    }  
}
