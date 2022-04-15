using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Implementations;

public static class LinePrinter
{
    public static void PrintLine(PdfData pdfData, int rowIndex, IReadOnlyList<string> wordsInRow, PdfDocumentBuilder.AddedFont font, double yOffset)
    {
        ColorUnEvenBackgroundGray(pdfData, yOffset, rowIndex);
        PrintWords(pdfData, wordsInRow, font, yOffset);
    }

    private static void ColorUnEvenBackgroundGray(PdfData pdfData, double yOffset, int rowIndex)
    {
        if (rowIndex % 2 == 0)
            return;
        var linePoint = new PdfPoint(0, pdfData.Page.PageSize.Top - yOffset - TherapyPlanConstants.BackgroundYOffset);
        pdfData.Page.SetTextAndFillColor(TherapyPlanConstants.GreyRgbValue, TherapyPlanConstants.GreyRgbValue, TherapyPlanConstants.GreyRgbValue);
        pdfData.Page.DrawRectangle(linePoint, TherapyPlanConstants.LineWidth, (decimal) TherapyPlanConstants.LineHeight, TherapyPlanConstants.BorderWidth, true);
        pdfData.Page.ResetColor();
    }

    private static void PrintWords(PdfData pdfData, IReadOnlyList<string> wordsInRow, PdfDocumentBuilder.AddedFont font, double yOffset)
    {
        for (var i = 0; i < wordsInRow.Count; i++)
        {
            var calculatedXOffset = CalculateXOffset(i);
            var point = new PdfPoint(calculatedXOffset, pdfData.Page.PageSize.Top - yOffset);
            pdfData.Page.AddText(wordsInRow[i], 5, point, font);
        }
    }

    private static double CalculateXOffset(int columnIndex)
    {
        var offset = TherapyPlanConstants.InitialXOffset + TherapyPlanConstants.ColumnWidth * columnIndex;
        if (columnIndex == 5)
            offset += 70d;

        return offset;
    }
}