using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapy;
using FluentAssertions;
using NSubstitute;
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
        var rooms = CreateRooms();
    }

    private IEnumerable<Room> CreateRooms()
    {
        var room1 = new Room("room1", 0)
            .AddOccupation(new Occupation(new )
    }
}
