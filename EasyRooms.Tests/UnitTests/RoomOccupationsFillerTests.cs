using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Rooms.Implementations;
using EasyRooms.Model.Rooms.Interfaces;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapies.Interfaces;
using NSubstitute;
using Xunit;

namespace EasyRooms.Tests.UnitTests;

public class RoomOccupationsFillerTests
{
    private readonly RoomOccupationsFiller _roomsOccupationsFiller;
    private readonly ITherapyFiller _therapyFiller;
    private readonly IRoomListCreator _roomListCreator;

    public RoomOccupationsFillerTests()
    {
        _therapyFiller = Substitute.For<ITherapyFiller>();
        _roomListCreator = Substitute.For<IRoomListCreator>();
        _roomsOccupationsFiller = new RoomOccupationsFiller(_therapyFiller, _roomListCreator);
    }

    [Fact]
    public void Fills_Room_Occupations()
    {
        var roomNames = CreateRoomNames();
        var expectedRooms = new List<Room>();
        var rows = CreateRows();
        _roomListCreator.CreateRooms(roomNames).Returns(expectedRooms);

        var _ = _roomsOccupationsFiller.FillRoomOccupations(rows, roomNames, 1);

        _roomListCreator.Received().CreateRooms(roomNames);
        _therapyFiller.Received().AddAllTherapies(expectedRooms, Arg.Is<List<Row>>(list => list.SequenceEqual(CreateExpectedRows())), 1, roomNames);
    }

    private static RoomNames CreateRoomNames() 
        => new("room1\nroom2", "room2", "room1");

    private static IEnumerable<Row> CreateRows()
        => new List<Row>
        {
            new("09:00", "2", "short", "long", "patient", "therapist"),
            new("08:00", "2", "short2", "long2", "patient2", "therapist2"),
        };
        
    private static IEnumerable<Row> CreateExpectedRows()
        => new List<Row>
        {
            new("08:00", "2", "short2", "long2", "patient2", "therapist2"),
            new("09:00", "2", "short", "long", "patient", "therapist"),
        };
}