namespace EasyRooms.Model.Rooms.Interfaces;

public interface IRoomNamesService
{
    void SaveRooms();
    RoomNames Rooms { get; set; }
}