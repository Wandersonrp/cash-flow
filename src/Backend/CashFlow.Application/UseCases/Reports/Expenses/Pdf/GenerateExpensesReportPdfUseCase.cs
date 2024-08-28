using CashFlow.Application.UseCases.Reports.Expenses.Pdf.Fonts;
using CashFlow.Domain.Repositories.Expenses;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Reports.Expenses.Pdf;
public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdf
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpenseRepository _expenseRepository;

    public GenerateExpensesReportPdfUseCase(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _expenseRepository.FilterByMonth(month);

        if(expenses.Count == 0)
        {
            return [];
        }

        return [];
    }
}
