using CashFlow.Communication.Requests.Pagination;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public class GetAllExpensesValidator : AbstractValidator<RequestPaginationJson>
{
    public GetAllExpensesValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.PAGE_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(x => x.ItemsPerPage)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.ITEMS_PER_PAGE_MUST_BE_GREATER_THAN_ZERO);
    }
}
