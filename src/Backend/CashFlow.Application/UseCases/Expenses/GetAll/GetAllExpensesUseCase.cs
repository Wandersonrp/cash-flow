using AutoMapper;
using CashFlow.Communication.Requests.Pagination;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Communication.Responses.Pagination;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public class GetAllExpensesUseCase : IGetAllExpenses
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllExpensesUseCase(IExpenseRepository expenseRepository, IMapper mapper, ILoggedUser loggedUser)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseGetAllExpensesJson> Execute(RequestPaginationJson pagination)
    {
        Validate(pagination);

        var user = await _loggedUser.Get();

        (List<Expense> expenses, int count) tuple = await _expenseRepository
            .GetAllAsync(pagination.Page, pagination.ItemsPerPage, user.Id);
        
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
