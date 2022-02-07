using System;

namespace EasyRooms.Model.Rooms.Interfaces;

public interface IFreeRoomFinder
{
    Room FindFreeRoom(TimeSpan startTime, TimeSpan endTime, int bufferInMinutes, IEnumerable<Room> rooms);
}