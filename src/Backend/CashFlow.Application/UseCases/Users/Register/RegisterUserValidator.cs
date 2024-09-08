using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Validators.Users;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()            
            .WithMessage(ResourceErrorMessages.NAME_EMPTY);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_EMAIL)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.INVALID_EMAIL);

        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}
