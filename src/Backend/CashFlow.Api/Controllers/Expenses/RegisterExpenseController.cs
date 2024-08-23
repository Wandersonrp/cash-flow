using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Expenses;

[ApiController]
[Route("api/expenses")]
public class RegisterExpenseController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResponseRegisterExpenseJson>> Register([FromBody] RequestRegisterExpenseJson request)
    {   
        var useCase = new RegisterExpenseUseCase();

        var response = await useCase.Execute(request);

        return CreatedAtAction(nameof(Register), response);
    }    
}
