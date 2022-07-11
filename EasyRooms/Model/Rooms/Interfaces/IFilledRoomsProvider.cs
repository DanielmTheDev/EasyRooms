namespace EasyRooms.Model.Rooms.Interfaces;

public interface IFilledRoomsProvider
{
    RoomsWithDate Get(string fileName);
}