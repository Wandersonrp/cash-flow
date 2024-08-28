namespace CashFlow.Application.UseCases.Reports.Expenses.Excel;
public interface IGenerateExpensesReportExcel
{
    Task<byte[]> Execute(DateOnly month);
}
