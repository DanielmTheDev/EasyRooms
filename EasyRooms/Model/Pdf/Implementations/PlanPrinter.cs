using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Core;

namespace EasyRooms.Model.Pdf.Implementations;

public class PlanPrinter : IPlanPrinter
{
    public void PrintHeaders(PdfData pdfData, double yOffset)
    {
        var headerStrings = new [] { "Beginn", "Dauer", "Raum", "Behandlung", string.Empty, "Patient", "Personal" };
        LinePrinter.PrintLine(pdfData, headerStrings, pdfData.BoldFont, yOffset);
    }

    public void PrintPageHeader(PdfData pdfData, string content, double headersYOffset)
    {
        var point = new PdfPoint(250d, pdfData.Page.PageSize.Top - headersYOffset);
        pdfData.Page.AddText(content, 15, point, pdfData.BoldFont);
    }
}