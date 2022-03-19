using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface IPdfCreator
{
    IEnumerable<PdfData> Create(IEnumerable<Room> rooms);
}