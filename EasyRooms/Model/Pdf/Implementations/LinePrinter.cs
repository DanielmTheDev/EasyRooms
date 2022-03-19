using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Implementations;

public static class LinePrinter
{
    public static void PrintLine(PdfData pdfData, IReadOnlyList<string> rowStrings, PdfDocumentBuilder.AddedFont font, double yOffset)
    {
        for (var i = 0; i < rowStrings.Count; i++)
        {
            var calculatedXOffset = CalculateXOffset(i);
            var point = new PdfPoint(calculatedXOffset, pdfData.Page.PageSize.Top - yOffset);
            pdfData.Page.AddText(rowStrings[i], 5, point, font);
        }
    }

    private static double CalculateXOffset(int i)
    {
        var offset = TherapyPlanConstants.InitialXOffset + TherapyPlanConstants.XOffset * i;
        switch (i)
        {
            case 5:
                offset += 70d;
                break;
            case 6:
                offset += 90d;
                break;
        }

        return offset;
    }
}