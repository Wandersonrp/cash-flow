using CashFlow.Application.UseCases.Reports.Expenses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.Api.Controllers.Reports.Expenses;

[Route("api/reports/expenses/excel")]
[ApiController]
public class ExpenseReportsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromQuery] DateOnly month, [FromServices] IGenerateExpensesReportExcel useCase)
    {
        var file = await useCase.Execute(month);

        if (file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        }

        return NoContent();
    }
}
