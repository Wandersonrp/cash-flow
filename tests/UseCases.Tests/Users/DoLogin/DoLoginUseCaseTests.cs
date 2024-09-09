using CashFlow.Application.UseCases.Users.DoLogin;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Commom.Test.Utilities.Cryptography;
using Commom.Test.Utilities.Entities;
using Commom.Test.Utilities.Repository;
using Commom.Test.Utilities.Requests.Users;
using Commom.Test.Utilities.Token;
using FluentAssertions;

namespace UseCases.Tests.Users.DoLogin;
public class DoLoginUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestLoginUserJsonBuilder.Build();

        var user = UserBuilder.Build();

        request.Email = user.Email;

        var useCase = CreateUseCase(user, request.Password);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }


    [Fact]
    public async Task Error_User_Not_Found()
    {
        var user = UserBuilder.Build();

        var request = RequestLoginUserJsonBuilder.Build();  

        var useCase = CreateUseCase(user, request.Password);    

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidCredentialsException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.INVALID_CREDENTIALS));
    }

    [Fact]
    public async Task Error_Password_Not_Macth()
    {
        var user = UserBuilder.Build();

        var request = RequestLoginUserJsonBuilder.Build();

        request.Email = user.Email;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidCredentialsException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.INVALID_CREDENTIALS));
    }


    private DoLoginUseCase CreateUseCase(User user, string? password = null)
    {
        var userRepository = new UserRepositoryBuilder().GetByEmail(user).Build();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var passwordComparer = new PasswordEcrypterBuilder();

        if(!string.IsNullOrWhiteSpace(password))
        {
            passwordComparer.Comparer(password).BuildComparer();
        }
                
        return new DoLoginUseCase(userRepository, accessTokenGenerator, passwordComparer.BuildComparer());
    }
}
