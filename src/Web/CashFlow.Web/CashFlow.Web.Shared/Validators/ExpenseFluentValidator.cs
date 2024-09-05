using CashFlow.Communication.Requests.Expenses;
using FluentValidation;

namespace CashFlow.Web.Shared.Validators;
public class ExpenseFluentValidator : AbstractValidator<RequestExpenseJson>
{
    public ExpenseFluentValidator()
    {
        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage("O título da despesa é obrigatório")
            .MaximumLength(30)
            .WithMessage("O título da despesa deve conter no máximo 30 caracteres");        
                       
        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data da despesa não pode ser futura");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("O valor da despesa deve ser maior que zero");

        RuleFor(x => x.PaymentType)
            .IsInEnum()
            .WithMessage("Tipo de pagamento inválido");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<RequestExpenseJson>.CreateWithOptions((RequestExpenseJson)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
