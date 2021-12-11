using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf;

public interface ITherapyPlanHeadersPrinter
{
    void PrintHeaders(PdfBuilderAggregate pdfBuilderAggregate, double yOffset);
}