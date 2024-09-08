using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Communication.Responses.Errors;
using CashFlow.Communication.Responses.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Expenses;

[Route("api/expenses")]
[ApiController]
public class GetExpenseByIdController : ControllerBase
{
    private readonly IGetExpenseById _useCase;

    public GetExpenseByIdController(IGetExpenseById useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    [Route("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseExpenseJson>> GetById([FromRoute] int id)
    {
        var expense = await _useCase.Execute(id);

        return Ok(expense);
    }
}
