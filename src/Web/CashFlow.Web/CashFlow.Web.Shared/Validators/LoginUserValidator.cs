using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Validators.Users;
using FluentValidation;

namespace CashFlow.Web.Shared.Validators;
public class LoginUserValidator : AbstractValidator<RequestLoginUserJson>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail é obrigatório")
            .EmailAddress()
            .WithMessage("E-mail inválido");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória")
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
        var result = await ValidateAsync(ValidationContext<RequestLoginUserJson>.CreateWithOptions((RequestLoginUserJson)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
