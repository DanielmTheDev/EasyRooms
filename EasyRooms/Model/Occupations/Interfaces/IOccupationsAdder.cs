namespace EasyRooms.Model.Occupations.Interfaces;

public interface IOccupationsAdder
{
    void AddToFreeRoom(IEnumerable<Room> rooms, int bufferInMinutes, params Row[] rows);
    void AddToSpecificRoom(IEnumerable<Room> rooms, string roomName, params Row[] rows);
}