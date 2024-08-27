namespace CashFlow.Application.UseCases.Reports.Expenses;
public interface IGenerateExpensesReportExcel
{
    Task<byte[]> Execute(DateOnly month);
}
