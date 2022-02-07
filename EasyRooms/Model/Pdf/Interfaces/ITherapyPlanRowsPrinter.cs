using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface ITherapyPlanRowsPrinter
{
    void PrintRows(IEnumerable<Room> rooms, PdfBuilderAggregate pdfBuilderAggregate, double yOffset);
}