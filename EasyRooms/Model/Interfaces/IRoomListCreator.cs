using System.Collections.Generic;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Interfaces;

public interface IRoomListCreator
{
    List<Room> CreateRooms(RoomNames roomNames);
}