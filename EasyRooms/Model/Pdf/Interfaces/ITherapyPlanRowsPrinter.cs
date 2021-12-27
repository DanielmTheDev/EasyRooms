using System.Collections.Generic;
using EasyRooms.Model.Pdf.Models;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface ITherapyPlanRowsPrinter
{
    void PrintRows(IEnumerable<Room> rooms, PdfBuilderAggregate pdfBuilderAggregate, double yOffset);
}