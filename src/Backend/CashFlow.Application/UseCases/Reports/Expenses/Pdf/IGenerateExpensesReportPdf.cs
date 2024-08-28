namespace CashFlow.Application.UseCases.Reports.Expenses.Pdf;
public interface IGenerateExpensesReportPdf
{
    Task<byte[]> Execute(DateOnly month);
}
