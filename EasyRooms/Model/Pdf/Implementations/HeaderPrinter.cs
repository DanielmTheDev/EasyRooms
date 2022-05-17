using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Implementations;

public class HeaderPrinter : IHeaderPrinter
{
    public void PrintColumnHeaders(PdfAggregate pdfAggregate, double yOffset, PdfPageBuilder page)
    {
        var headerStrings = new [] { "Beginn", "Dauer", "Zusatz", "Raum", "Behandlung", "Patient*in" };
        LinePrinter.PrintLine(0, headerStrings, pdfAggregate.BoldFont, yOffset, page);
    }

    public void PrintPageHeader(PdfAggregate pdfAggregate, string content, double headersYOffset, PdfPageBuilder page)
    {
        var contentPoint = new PdfPoint(250d, page.PageSize.Top - headersYOffset);
        page.AddText(content, 15, contentPoint, pdfAggregate.BoldFont);
        var datePoint = new PdfPoint(40d, page.PageSize.Top - headersYOffset);
        page.AddText(DateOnly.FromDateTime(DateTime.Now).ToString(), TherapyPlanConstants.FontSize, datePoint, pdfAggregate.Font);
    }
}