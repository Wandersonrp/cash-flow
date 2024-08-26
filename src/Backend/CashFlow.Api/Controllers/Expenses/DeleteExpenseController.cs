using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Communication.Responses.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Expenses;

[Route("api/expenses")]
[ApiController]
public class DeleteExpenseController : ControllerBase
{
    private readonly IDeleteExpense _useCase;

    public DeleteExpenseController(IDeleteExpense useCase)
    {
        _useCase = useCase;
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _useCase.Execute(id);

        return NoContent();
    }
}
