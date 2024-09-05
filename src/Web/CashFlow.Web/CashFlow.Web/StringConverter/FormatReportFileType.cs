using CashFlow.Web.Client.Enums;

namespace CashFlow.Web.StringConverter;

public static class FormatReportFileType
{
    public static string ToFriendlyString(this ReportFileType reportFileType)
    {
        return reportFileType switch
        {
            ReportFileType.Pdf => "pdf",
            ReportFileType.Excel => "excel",
            _ => throw new ArgumentOutOfRangeException(nameof(reportFileType), reportFileType, null)
        };
    }
}
