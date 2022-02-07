namespace EasyRooms.Model.Pdf.Interfaces;

public interface IPdfWriter
{
    void Write(IEnumerable<Room> filledRooms);
}