using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Expenses;

[Route("api/expenses")]
[ApiController]
public class UpdateExpenseController : ControllerBase
{
    private readonly IUpdateExpense _useCase;

    public UpdateExpenseController(IUpdateExpense useCase)
    {
        _useCase = useCase;
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromBody] RequestExpenseJson request, 
        [FromRoute] int id)
    {
        await _useCase.Execute(id, request);

        return NoContent();
    }
}
