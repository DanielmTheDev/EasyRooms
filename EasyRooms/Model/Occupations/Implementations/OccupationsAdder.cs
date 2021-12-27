using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Occupations.Interfaces;
using EasyRooms.Model.Rooms.Interfaces;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Occupations.Implementations;

public class OccupationsAdder : IOccupationsAdder
{
    private readonly IFreeRoomFinder _freeRoomFinder;

    public OccupationsAdder(IFreeRoomFinder freeRoomFinder)
        => _freeRoomFinder = freeRoomFinder;

    public void AddToFreeRoom(IEnumerable<Room> rooms, int bufferInMinutes, params Row[] rows)
    {
        var freeRoom = _freeRoomFinder.FindFreeRoom(rows.First().StartTimeAsTimeSpan, rows.First().EndTimeAsTimeSpan, bufferInMinutes, rooms);
        foreach (var row in rows)
        {
            freeRoom.AddOccupation(new Occupation(row, row.StartTimeAsTimeSpan, row.EndTimeAsTimeSpan));
        }
    }

    public void AddToSpecificRoom(IEnumerable<Room> rooms, string roomName, params Row[] rows)
    {
        var specificRoom = rooms.Single(room => string.Equals(room.Name, roomName, StringComparison.OrdinalIgnoreCase));
        foreach (var row in rows)
        {
            specificRoom.AddOccupation(new Occupation(row, row.StartTimeAsTimeSpan, row.EndTimeAsTimeSpan));
        }
    }
}