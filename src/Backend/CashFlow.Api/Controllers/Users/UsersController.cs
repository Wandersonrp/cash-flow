using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUser useCase,
        [FromBody] RequestRegisterUserJson request
        )
    {
        await useCase.Execute(request);

        return CreatedAtAction(nameof(Register), string.Empty);
    }
}
