using System;

namespace EasyRooms.Model.Models;

public record OccupationCreationData(TimeSpan StartTime, TimeSpan EndTime, Room FreeRoom) { }