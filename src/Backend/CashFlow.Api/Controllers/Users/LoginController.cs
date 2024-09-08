using CashFlow.Application.UseCases.Users.DoLogin;
using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.Errors;
using CashFlow.Communication.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers.Users;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoginUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLogin useCase, [FromBody] RequestLoginUserJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}
