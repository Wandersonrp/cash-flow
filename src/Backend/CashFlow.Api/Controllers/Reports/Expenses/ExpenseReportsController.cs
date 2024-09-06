using CashFlow.Application.UseCases.Reports.Expenses.Excel;
using CashFlow.Application.UseCases.Reports.Expenses.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.Api.Controllers.Reports.Expenses;

[Route("api/reports/expenses/")]
[ApiController]
public class ExpenseReportsController : ControllerBase
{
    [HttpGet]
    [Route("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExcel([FromQuery] DateOnly month, [FromServices] IGenerateExpensesReportExcel useCase)
    {
        var file = await useCase.Execute(month);

        if (file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        }

        return NotFound();
    }

    [HttpGet]
    [Route("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPdf([FromQuery] DateOnly month, [FromServices] IGenerateExpensesReportPdf useCase)
    {
        var file = await useCase.Execute(month);

        if (file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
        }

        return NotFound();
    }
}
