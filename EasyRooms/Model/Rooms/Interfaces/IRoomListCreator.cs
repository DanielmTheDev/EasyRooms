namespace EasyRooms.Model.Rooms.Interfaces;

public interface IRoomListCreator
{
    List<Room> CreateRooms(RoomNames roomNames);
}