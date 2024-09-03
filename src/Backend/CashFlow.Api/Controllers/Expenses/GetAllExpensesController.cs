using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Communication.Requests.Pagination;
using CashFlow.Communication.Responses.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Expenses;

[Route("api/expenses")]
[ApiController]
public class GetAllExpensesController : ControllerBase
{
    private readonly IGetAllExpenses _useCase;

    public GetAllExpensesController(IGetAllExpenses useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseExpenseJson>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ResponseExpenseJson>>> GetAll(
        [FromQuery] RequestPaginationJson pagination)
    {
        var result = await _useCase.Execute(pagination);

        return Ok(result);
    }
}
