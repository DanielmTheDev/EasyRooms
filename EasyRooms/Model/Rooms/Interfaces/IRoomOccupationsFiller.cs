namespace EasyRooms.Model.Rooms.Interfaces;

public interface IRoomOccupationsFiller
{
    IEnumerable<Room> FillRoomOccupations(IEnumerable<Row> rows, RoomNames roomNames, int bufferInMinutes = 0);
}