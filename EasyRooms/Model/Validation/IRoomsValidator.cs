using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Validation;

public interface IRoomsValidator
{
    bool IsValid(IEnumerable<Room> rooms, RoomNames roomNames);
}