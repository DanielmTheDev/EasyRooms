namespace EasyRooms.Model.Therapies.Interfaces;

public interface ITherapiesAdder
{
    void Add(IEnumerable<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames);
}