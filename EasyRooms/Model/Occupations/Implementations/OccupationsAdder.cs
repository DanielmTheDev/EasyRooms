using System;
using EasyRooms.Model.Rooms.Interfaces;

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
        //todo this method should get the room as parameter, instead of all the rooms and the roomname
        var specificRoom = rooms.Single(room => string.Equals(room.Name, roomName, StringComparison.OrdinalIgnoreCase));
        foreach (var row in rows)
        {
            specificRoom.AddOccupation(new Occupation(row, row.StartTimeAsTimeSpan, row.EndTimeAsTimeSpan));
        }
    }
}