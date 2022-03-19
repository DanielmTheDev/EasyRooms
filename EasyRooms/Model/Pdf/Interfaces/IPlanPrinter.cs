using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface IPlanPrinter
{
    void PrintHeaders(Models.PdfData pdfData, double yOffset);
    void PrintPageHeader(PdfData pdfData, string content, double headersYOffset);
}