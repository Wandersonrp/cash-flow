using CashFlow.Application.Services.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Count;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.SumTotal;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Reports.Expenses.Excel;
using CashFlow.Application.UseCases.Reports.Expenses.Pdf;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services
            .AddScoped<IRegisterExpense, RegisterExpenseUseCase>()
            .AddScoped<IGetAllExpenses, GetAllExpensesUseCase>()
            .AddScoped<IGetExpenseById, GetExpenseByIdUseCase>()
            .AddScoped<IDeleteExpense, DeleteExpenseUseCase>()
            .AddScoped<IUpdateExpense, UpdateExpenseUseCase>()
            .AddScoped<IGenerateExpensesReportExcel, GenerateExpensesReportExcelUseCase>()
            .AddScoped<IGenerateExpensesReportPdf, GenerateExpensesReportPdfUseCase>()
            .AddScoped<ICountExpenses, CountExpensesUseCase>()
            .AddScoped<IExpenseSumTotal, ExpenseSumTotalUseCase>();
    }
}
