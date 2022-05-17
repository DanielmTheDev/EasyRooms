using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface IPdfCreator
{
    PdfAggregate Create(IEnumerable<Room> rooms);
}