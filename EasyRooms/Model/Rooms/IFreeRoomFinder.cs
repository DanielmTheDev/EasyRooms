using System;
using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Rooms;

public interface IFreeRoomFinder
{
    Room FindFreeRoom(TimeSpan startTime, TimeSpan endTime, int bufferInMinutes, IEnumerable<Room> rooms);
}