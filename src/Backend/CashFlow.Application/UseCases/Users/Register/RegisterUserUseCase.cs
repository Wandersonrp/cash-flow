using AutoMapper;
using CashFlow.Communication.Requests.Users;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.UnitOfWork;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUser
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
        IUserRepository userRepository, 
        IMapper mapper, 
        IPasswordEncrypter passwordEncrypter, 
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();        

        await _userRepository.AddAsync(user);

        await _unitOfWork.Commit();        
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var userExistsWithEmail = await _userRepository.ExistsActiveUserWithEmail(request.Email);

        if(userExistsWithEmail)
        {
            throw new ConflictException(ResourceErrorMessages.EMAIL_ALREADY_EXISTS);
        }

        if (!result.IsValid)
        {
            var errorMessages = result
                .Errors
                .Select(x => x.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
