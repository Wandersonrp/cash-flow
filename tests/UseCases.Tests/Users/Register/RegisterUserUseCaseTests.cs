using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Commom.Test.Utilities.Cryptography;
using Commom.Test.Utilities.Mapper;
using Commom.Test.Utilities.Repository;
using Commom.Test.Utilities.Requests.Users;
using FluentAssertions;

namespace UseCases.Tests.Users.Register;
public class RegisterUserUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        await useCase.Execute(request);        
    }

    [Fact]
    public async Task Error_Empty_Name()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(exception => exception.GetErrors().Count == 1 && exception.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Email_Already_Exists()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ConflictException>();

        result.Where(exception => exception.GetErrors().Count == 1 && exception.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userRepository = new UserRepositoryBuilder();
        var passwordEncrypter = new PasswordEcrypterBuilder().BuildEncrypter();

        if (!string.IsNullOrWhiteSpace(email))
        {
            userRepository.ExistsActiveUserWithEmail(email);
        }

        return new RegisterUserUseCase(userRepository.Build(), mapper, passwordEncrypter, unitOfWork);
    }
}
