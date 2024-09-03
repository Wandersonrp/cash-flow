using AutoMapper;
using CashFlow.Communication.Requests.Pagination;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Communication.Responses.Pagination;
using CashFlow.Domain.Entities;
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

    public async Task<ResponseGetAllExpensesJson> Execute(RequestPaginationJson pagination)
    {
        Validate(pagination);

        (List<Expense> expenses, int count) tuple = await _expenseRepository
            .GetAllAsync(pagination.Page, pagination.ItemsPerPage);
        

        return new ResponseGetAllExpensesJson
        {
            Expenses = tuple.expenses.Select(expense => _mapper.Map<ResponseExpenseJson>(expense)).ToList(),
            Pagination = new ResponsePaginationJson
            {
                Page = pagination.Page,
                ItemsPerPage = pagination.ItemsPerPage,
                TotalItems = tuple.count, 
                TotalPages = Math.Ceiling((double) tuple.count / pagination.ItemsPerPage),
            }
        };
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
