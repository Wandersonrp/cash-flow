using AutoMapper;
using CashFlow.Communication.Requests.Pagination;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public class GetAllExpensesUseCase : IGetAllExpenses
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetAllExpensesUseCase(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<List<ResponseExpenseJson>> Execute(RequestPaginationJson pagination)
    {
        Validate(pagination);

        var expenses = await _expenseRepository
            .GetAllAsync(pagination.Page ?? 1, pagination.ItemsPerPage ?? 5);

        var response = _mapper
            .Map<List<ResponseExpenseJson>>(expenses);

        return response;
    }

    private void Validate(RequestPaginationJson pagination)
    {
        var validator = new GetAllExpensesValidator();

        var result = validator.Validate(pagination);

        if(!result.IsValid)
        {
            var errorMessages = result
                .Errors
                .Select(x => x.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
