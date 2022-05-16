using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface IPdfCreator
{
    IEnumerable<PdfAggregate> Create(IEnumerable<Room> rooms, PdfAggregate pdf);
}