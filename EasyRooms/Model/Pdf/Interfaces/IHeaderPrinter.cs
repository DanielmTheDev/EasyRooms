using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface IHeaderPrinter
{
    void PrintColumnHeaders(PdfAggregate pdfAggregate, double yOffset, PdfPageBuilder page);
    void PrintPageHeader(PdfAggregate pdfAggregate, string content, double headersYOffset, PdfPageBuilder page, DateOnly date);
}