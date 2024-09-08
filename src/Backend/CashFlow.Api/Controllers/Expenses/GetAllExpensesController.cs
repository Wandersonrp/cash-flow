using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Communication.Requests.Pagination;
using CashFlow.Communication.Responses.Expenses;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ProducesResponseType(typeof(ResponseGetAllExpensesJson), StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseGetAllExpensesJson>> GetAll(
        [FromQuery] int? page, 
        [FromQuery] int? itemsPerPage)
    {
        var result = await _useCase.Execute(new RequestPaginationJson
        {
            Page = page ?? 1,
            ItemsPerPage = itemsPerPage ?? 5,
        });        

        return Ok(new 
        {
            result.Expenses,
            result.Pagination
        });
    }
}
