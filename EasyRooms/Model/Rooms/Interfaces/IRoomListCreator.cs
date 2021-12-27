using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Rooms.Interfaces;

public interface IRoomListCreator
{
    List<Room> CreateRooms(RoomNames roomNames);
}