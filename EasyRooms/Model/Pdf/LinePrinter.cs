using System.Collections.Generic;
using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf
{
    public static class LinePrinter
    {
        public static void PrintLine(PdfBuilderAggregate pdfBuilderAggregate, IReadOnlyList<string> rowStrings, PdfDocumentBuilder.AddedFont font, double yOffset)
        {
            for (var i = 0; i < rowStrings.Count; i++)
            {
                var calculatedOffset = TherapyPlanConstants.InitialXOffset + TherapyPlanConstants.XOffset * i;
                var point = new PdfPoint(calculatedOffset, pdfBuilderAggregate.Page.PageSize.Top - yOffset);
                pdfBuilderAggregate.Page.AddText(rowStrings[i], 5, point, font);
            }
        }
    }
}