﻿using CashFlow.Application.UseCases.Reports.Expenses.Pdf.Colors;
using CashFlow.Application.UseCases.Reports.Expenses.Pdf.Fonts;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Extensions;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Resources.Reports;
using CashFlow.Domain.Services.LoggedUser;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Reports.Expenses.Pdf;
public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdf
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_EXPENSE_TABLE = 25;
    private readonly IExpenseRepository _expenseRepository;
    private readonly ILoggedUser _loggedUser;

    public GenerateExpensesReportPdfUseCase(IExpenseRepository expenseRepository, ILoggedUser loggedUser)
    {
        _expenseRepository = expenseRepository;
        _loggedUser = loggedUser;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var user = await _loggedUser.Get();

        var expenses = await _expenseRepository.FilterByMonth(month, user.Id);

        if (expenses.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);

        CreateTotalSpentSection(page, month, expenses);

        foreach (var expense in expenses)
        {
            var table = CreateExpenseTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;

            AddExpenseTitle(row.Cells[0], expense.Title);

            AddHeaderAmount(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;

            row.Cells[0].AddParagraph(expense.Date.ToString("dd/MM/yyyy"));
            SetStyleBaseForExpenseInformation(row.Cells[0]);
            row.Cells[0].MergeRight = 1;
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[2].AddParagraph(expense.PaymentType.ToFormatString());
            SetStyleBaseForExpenseInformation(row.Cells[2]);

            AddExpenseAmount(row.Cells[3], expense.Amount);

            if(!string.IsNullOrWhiteSpace(expense.Description))
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_EXPENSE_TABLE;                

                AddExpenseDescription(descriptionRow.Cells[0], expense.Description);

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);

        }

        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} {month.ToString("Y")}";
        document.Info.Author = "CashFlow";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var stream = new MemoryStream();
        renderer.PdfDocument.Save(stream);

        return stream.ToArray();
    }

    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);

        var pathFile = Path.Combine(directoryName!, "UseCases/Reports/Expenses/Pdf/Logo", "qrcode.png");

        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph("Olá, Cash Flow");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Section page, DateOnly month, List<Expense> expenses)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = 40;
        paragraph.Format.SpaceAfter = 40;

        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

        paragraph.AddLineBreak();

        var totalExpenses = expenses.Sum(expense => expense.Amount);

        paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalExpenses}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private void AddExpenseTitle(Cell cell, string expenseTitle)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
        cell.AddParagraph(expenseTitle);
        cell.Shading.Color = ColorsHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }

    private void AddHeaderAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForExpenseInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddExpenseAmount(Cell cell, decimal expenseAmount)
    {
        cell.AddParagraph($"{CURRENCY_SYMBOL} -{expenseAmount}");
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private void AddExpenseDescription(Cell cell, string expenseDescription)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
        cell.AddParagraph(expenseDescription);
        cell.Shading.Color = ColorsHelper.GREEN_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;        
        cell.Format.LeftIndent = 20;
    }
}
