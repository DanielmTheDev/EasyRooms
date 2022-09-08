namespace EasyRooms.Model.Persistence.Interfaces;

public interface IPlainListWriter
{
    void Write(IEnumerable<Room> rooms);
}