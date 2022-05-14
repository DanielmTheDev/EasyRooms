using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface IHeaderPrinter
{
    void PrintColumnHeaders(Models.PdfData pdfData, double yOffset);
    void PrintPageHeader(PdfData pdfData, string content, double headersYOffset);
}