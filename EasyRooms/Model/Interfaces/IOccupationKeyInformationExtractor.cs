using System;
using System.Collections.Generic;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Interfaces;

public interface IOccupationKeyInformationExtractor
{
    (TimeSpan startTime, TimeSpan endTime, Room freeRoom) GetOccupationInformation(string startTimeString, string duration, int bufferInMinutes, List<Room> rooms);
}