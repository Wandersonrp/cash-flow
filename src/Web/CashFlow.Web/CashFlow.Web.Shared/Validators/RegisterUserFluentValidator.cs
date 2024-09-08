using CashFlow.Communication.Requests.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CashFlow.Web.Shared.Validators;
public class RegisterUserFluentValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserFluentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail é obrigatório")
            .EmailAddress()
            .WithMessage("E-mail inválido");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatório")
            .MinimumLength(8)
            .WithMessage("Senha deve conter no mínimo 8 caracteres")
            .MaximumLength(20)
            .WithMessage("Senha deve conter no máximo 20 caracteres")
            .Matches(@"[A-Z]+")
            .WithMessage("Senha deve conter pelo menos uma letra maiúscula")
            .Matches(@"[a-z]+")
            .WithMessage("Senha deve conter pelo menos uma letra minúscula")
            .Matches(@"[0-9]+")
            .WithMessage("Senha deve conter pelo menos um número")
            .Matches(@"[\!\?\*\.]+")
            .WithMessage("Senha deve conter pelo menos um caractere especial");
    }   

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<RequestRegisterUserJson>.CreateWithOptions((RequestRegisterUserJson)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
