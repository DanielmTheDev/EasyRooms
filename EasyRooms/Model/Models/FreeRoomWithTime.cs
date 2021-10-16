using System;

namespace EasyRooms.Model.Models;

public record FreeRoomWithTime(TimeSpan StartTime, TimeSpan EndTime, Room FreeRoom) { }