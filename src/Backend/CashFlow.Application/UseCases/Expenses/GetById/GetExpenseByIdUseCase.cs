using AutoMapper;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetExpenseByIdUseCase : IGetExpenseById
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetExpenseByIdUseCase(
        IExpenseRepository expenseRepository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseExpenseJson> Execute(int id)
    {
        var user = await _loggedUser.Get();

        var expense = await _expenseRepository.GetByIdAsync(id, user.Id);

        if(expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        return _mapper.Map<ResponseExpenseJson>(expense);
    }
}
