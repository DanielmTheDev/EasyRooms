using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Interfaces;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Implementations;

public class OccupationCreationDataProvider : IOccupationCreationDataProvider
{
    public FreeRoomWithTime CalculateOccupationCreationData(string startTimeString, string duration, int bufferInMinutes, List<Room> rooms)
    {
        //todo this trimming is a workaround. In reality, such a case probably has to be put into the same room as the
        //theray that came before, since it means something like preparation
        var startTime = TimeSpan.Parse(startTimeString.Trim('(', ')'));
        var endTime = AddDurationAsMinutes(duration, startTime);
        var freeRoom = rooms.First(room => !room.IsOccupiedAt(startTime, endTime, bufferInMinutes));
        return new FreeRoomWithTime(startTime, endTime, freeRoom);
    }

    private static TimeSpan AddDurationAsMinutes(string duration, TimeSpan startTime)
    {
        var minutes = TimeSpan.FromMinutes(int.Parse(duration));
        return startTime.Add(minutes);
    }
}
