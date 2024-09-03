using CashFlow.Application.UseCases.Expenses.SumTotal;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Expenses;

[Route("api/expenses/sum")]
[ApiController]
public class ExpenseSumTotalController : ControllerBase
{
    private readonly IExpenseSumTotal _useCase;

    public ExpenseSumTotalController(IExpenseSumTotal useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    public async Task<ActionResult<decimal>> SumAsync()
    {
        var result = await _useCase.Execute();

        return Ok(result);
    }
}
