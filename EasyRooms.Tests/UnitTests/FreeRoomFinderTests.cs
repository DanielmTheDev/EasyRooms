using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using FluentAssertions;
using Xunit;

namespace EasyRooms.Tests.UnitTests;

public class FreeRoomFinderTests
{
    private readonly FreeRoomFinder _freeRoomFinder;

    public FreeRoomFinderTests()
        => _freeRoomFinder = new FreeRoomFinder();

    [Fact]
    public void Finds_First_Free_Room()
    {
        var rooms = CreateRooms().ToList();
        var expectedFreeRoom = new FreeRoomWithTime(new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0), rooms.Last());
        var freeRoom = _freeRoomFinder.FindFreeRoom("08:00", "120", 0, rooms);
        freeRoom.Should().BeEquivalentTo(expectedFreeRoom);
    }

    private IEnumerable<Room> CreateRooms()
    {
        var row1 = CreateDefaultRow();
        var room1 = new Room("room1", 0).AddOccupation(new Occupation(row1, new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0)));
        var room2 = new Room("room2", 1).AddOccupation(new Occupation(row1, new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0)));
        var room3 = new Room("room3", 2);
        return new List<Room> {room1, room2, room3};
    }

    private Row CreateDefaultRow()
        => new("", "", "sh", "lo", "patient", "therapist");
}