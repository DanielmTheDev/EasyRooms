using System;

namespace EasyRooms.Model.Rooms.Models;

public record FreeRoomWithTime(TimeSpan StartTime, TimeSpan EndTime, Room FreeRoom);