namespace EasyRooms.Model.Rooms.Interfaces;

public interface IRoomListCreator
{
    IList<Room> CreateRooms(RoomNames roomNames);
}