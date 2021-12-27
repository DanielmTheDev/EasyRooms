using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Rooms.Exceptions;
using EasyRooms.Model.Rooms.Interfaces;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Rooms.Implementations;

public class FreeRoomFinder : IFreeRoomFinder
{
    public Room FindFreeRoom(TimeSpan startTime, TimeSpan endTime, int bufferInMinutes, IEnumerable<Room> rooms)
    {
        var enumeratedRooms = rooms.ToList();
        for (; bufferInMinutes >= 0; bufferInMinutes--)
        {
            var freeRoom = enumeratedRooms.FirstOrDefault(room => !room.IsOccupiedAt(startTime, endTime, bufferInMinutes));
            if (freeRoom is { })
            {
                return freeRoom;
            }
        }

        throw new NoFreeRoomException();
    }
}