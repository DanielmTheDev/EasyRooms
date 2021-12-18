using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Rooms;

public class FreeRoomFinder : IFreeRoomFinder
{
    public Room FindFreeRoom(TimeSpan startTime, TimeSpan endTime, int bufferInMinutes, IEnumerable<Room> rooms)
        => rooms.First(room => !room.IsOccupiedAt(startTime, endTime, bufferInMinutes));
}