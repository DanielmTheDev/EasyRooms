using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Core;

namespace EasyRooms.Model.Pdf.Implementations;

public class HeaderPrinter : IHeaderPrinter
{
    public void PrintColumnHeaders(PdfData pdfData, double yOffset)
    {
        var headerStrings = new [] { "Beginn", "Dauer", "Zusatz", "Raum", "Behandlung", "Patient" };
        LinePrinter.PrintLine(pdfData, 0, headerStrings, pdfData.BoldFont, yOffset);
    }

    public void PrintPageHeader(PdfData pdfData, string content, double headersYOffset)
    {
        var contentPoint = new PdfPoint(250d, pdfData.Page.PageSize.Top - headersYOffset);
        pdfData.Page.AddText(content, 15, contentPoint, pdfData.BoldFont);
        var datePoint = new PdfPoint(40d, pdfData.Page.PageSize.Top - headersYOffset);
        pdfData.Page.AddText(DateOnly.FromDateTime(DateTime.Now).ToString(), TherapyPlanConstants.FontSize, datePoint, pdfData.Font);
    }
}