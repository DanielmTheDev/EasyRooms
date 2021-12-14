using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Occupations;

public class OccupationsAdder : IOccupationsAdder
{
    private readonly IFreeRoomFinder _freeRoomFinder;

    public OccupationsAdder(IFreeRoomFinder freeRoomFinder)
        => _freeRoomFinder = freeRoomFinder;

    public void AddToFreeRoom(IEnumerable<Room> rooms, int bufferInMinutes, params Row[] rows)
    {
        var (startTime, timeSpan, freeRoom) = _freeRoomFinder.FindFreeRoom(rows.First().StartTime, rows.First().Duration, bufferInMinutes, rooms);
        foreach (var row in rows)
        {
            freeRoom.AddOccupation(new Occupation(row, startTime, timeSpan));
        }
    }

    public void AddToSpecificRoom(IEnumerable<Room> rooms, string roomName, params Row[] rows)
    {
        var specificRoom = rooms.Single(room => string.Equals(room.Name, roomName, StringComparison.OrdinalIgnoreCase));
        foreach (var row in rows)
        {
            var startTime = TimeSpan.Parse(row.StartTime.Trim('(', ')'));
            var endTime = startTime + TimeSpan.FromMinutes(int.Parse(row.Duration));
            specificRoom.AddOccupation(new Occupation(row, startTime, endTime));
        }
    }
}