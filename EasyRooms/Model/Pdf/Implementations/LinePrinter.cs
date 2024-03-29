using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Implementations;

public static class LinePrinter
{
    public static void PrintLine(int rowIndex, IReadOnlyList<string> wordsInRow, PdfDocumentBuilder.AddedFont font, double yOffset, PdfPageBuilder page)
    {
        ColorBackground(yOffset, rowIndex, page);
        PrintWords(wordsInRow, font, yOffset, page);
    }

    private static void ColorBackground(double yOffset, int rowIndex, PdfPageBuilder page)
    {
        if (rowIndex % 2 == 0)
            return;
        page.SetTextAndFillColor(TherapyPlanConstants.GreyRgbValue, TherapyPlanConstants.GreyRgbValue, TherapyPlanConstants.GreyRgbValue);
        DrawBackground(yOffset, page);
        page.ResetColor();
    }

    private static void DrawBackground(double yOffset, PdfPageBuilder page)
    {
        var linePoint = new PdfPoint(0, page.PageSize.Top - yOffset - TherapyPlanConstants.BackgroundYOffset);
        page.DrawRectangle(linePoint, TherapyPlanConstants.LineWidth, (decimal) TherapyPlanConstants.LineHeight, TherapyPlanConstants.BorderWidth, true);
    }

    private static void PrintWords(IReadOnlyList<string> wordsInRow, PdfDocumentBuilder.AddedFont font, double yOffset, PdfPageBuilder page)
    {
        for (var i = 0; i < wordsInRow.Count; i++)
        {
            var calculatedXOffset = CalculateXOffset(i);
            var point = new PdfPoint(calculatedXOffset, page.PageSize.Top - yOffset);
            page.AddText(wordsInRow[i], TherapyPlanConstants.FontSize, point, font);
        }
    }

    private static double CalculateXOffset(int columnIndex)
    {
        var extraWidth = columnIndex switch
        {
            1 => -30d,
            2 => -60d,
            3 => 40d,
            _ => 0d,
        };
        return TherapyPlanConstants.InitialXOffset + TherapyPlanConstants.ColumnWidth * columnIndex + extraWidth;
    }
}