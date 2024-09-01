﻿using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Reports.Expenses.Pdf.Fonts;
public class ExpensesReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        if (stream is null)
        {
            stream = ReadFontFile(FontHelper.DEFAULT_FONT);
        }

        var length = (int)stream!.Length;

        var data = new byte[length];

        stream.Read(data, 0, length);

        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"CashFlow.Application.UseCases.Reports.Expenses.Pdf.Fonts.{faceName}.ttf");
    }
}
