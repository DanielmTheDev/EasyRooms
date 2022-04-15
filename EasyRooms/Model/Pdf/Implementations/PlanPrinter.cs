using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Core;

namespace EasyRooms.Model.Pdf.Implementations;

public class PlanPrinter : IPlanPrinter
{
    public void PrintHeaders(PdfData pdfData, double yOffset)
    {
        var headerStrings = new [] { "Beginn", "Dauer", "Raum", "Behandlung", string.Empty, "Patient" };
        LinePrinter.PrintLine(pdfData, 0, headerStrings, pdfData.BoldFont, yOffset);
    }

    public void PrintPageHeader(PdfData pdfData, string content, double headersYOffset)
    {
        var contentPoint = new PdfPoint(250d, pdfData.Page.PageSize.Top - headersYOffset);
        pdfData.Page.AddText(content, 15, contentPoint, pdfData.BoldFont);
        var datePoint = new PdfPoint(40d, pdfData.Page.PageSize.Top - headersYOffset);
        pdfData.Page.AddText(DateOnly.FromDateTime(DateTime.Now).ToString(), 8, datePoint, pdfData.Font);
    }
}