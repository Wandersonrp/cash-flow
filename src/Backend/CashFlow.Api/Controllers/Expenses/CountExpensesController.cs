using CashFlow.Application.UseCases.Expenses.Count;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Expenses;

[Route("api/expenses/count")]
[ApiController]
public class CountExpensesController : ControllerBase
{
    private readonly ICountExpenses _useCase;

    public CountExpensesController(ICountExpenses useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> CountExpenses()
    {
        var count = await _useCase.Execute();

        return Ok(count);
    }
}
