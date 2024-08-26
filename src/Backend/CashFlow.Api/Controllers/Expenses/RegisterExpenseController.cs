using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Errors;
using CashFlow.Communication.Responses.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Expenses;

[ApiController]
[Route("api/expenses")]
public class RegisterExpenseController : ControllerBase
{
    private readonly IRegisterExpense _useCase;

    public RegisterExpenseController(IRegisterExpense useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResponseRegisterExpenseJson>> Register([FromBody] RequestExpenseJson request)
    {           
        var response = await _useCase.Execute(request);

        return CreatedAtAction(nameof(Register), response);
    }    
}
