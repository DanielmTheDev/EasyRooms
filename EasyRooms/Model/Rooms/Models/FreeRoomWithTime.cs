using System;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Rooms.Models
{
    public record FreeRoomWithTime(TimeSpan StartTime, TimeSpan EndTime, Room FreeRoom);
}