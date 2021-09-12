using EasyRooms.Model.Implementations;
using EasyRooms.Model.Models;
using FluentAssertions;
using Xunit;

namespace EasyRooms.Tests.UnitTests;

public class RoomOccupationsFillterTests
{
    private readonly RoomOccupationsFiller _roomsOccupationsFiller;

    public RoomOccupationsFillterTests()
        => _roomsOccupationsFiller = new RoomOccupationsFiller();

    [Fact]
    public void Distrubutes_Overlapping_Rows_Between_Two_Rooms()
    {
        var rows = new[]
        {
                new Row("08:00", "240", "short", "long", "Hans", "Wadim"),
                new Row("10:00", "240", "short2", "long2", "Carmen", "Dani"),
            };
        var expectedRoom1 = new Room("room1", 0).AddOccupation(new Occupation("Wadim", "Hans", "short", "long", new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0)));
        var expectedRoom2 = new Room("room2", 1).AddOccupation(new Occupation("Dani", "Carmen", "short2", "long2", new TimeSpan(10, 0, 0), new TimeSpan(14, 0, 0)));

        var resultRooms = _roomsOccupationsFiller.FillRoomOccupations(rows, new[] { "room1", "room2" }).ToList();

        resultRooms[0].Occupations.Should().BeEquivalentTo(expectedRoom1.Occupations);
        resultRooms[1].Occupations.Should().BeEquivalentTo(expectedRoom2.Occupations);
    }

    [Fact]
    public void Distrubutes_Overlapping_Rows_Between_Two_Rooms_With_Buffer()
    {
        var rows = new[]
        {
                new Row("08:00", "55", "short", "long", "Hans", "Wadim"),
                new Row("09:00", "60", "short2", "long2", "Carmen", "Dani"),
            };
        var expectedRoom1 = new Room("room1", 0).AddOccupation(new Occupation("Wadim", "Hans", "short", "long", new TimeSpan(8, 0, 0), new TimeSpan(8, 55, 0)));
        var expectedRoom2 = new Room("room2", 1).AddOccupation(new Occupation("Dani", "Carmen", "short2", "long2", new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0)));

        var resultRooms = _roomsOccupationsFiller.FillRoomOccupations(rows, new[] { "room1", "room2" }, 10).ToList();

        resultRooms[0].Occupations.Should().BeEquivalentTo(expectedRoom1.Occupations);
        resultRooms[1].Occupations.Should().BeEquivalentTo(expectedRoom2.Occupations);
    }

    [Fact]
    public void Partner_Massages_Get_Priority_Over_All_Others()
    {
        var rows = new[]
        {
                new Row("09:00", "60", "short2", "long2", "Carmen", "Dani"),
                new Row("09:00", "60", "*PARTNER", "long", "PartnerPatient", "Partner1"),
                new Row("09:00", "60", "*PARTNER", "long", "PartnerPatient", "Partner2")
            };
        var expectedRoom1 = new Room("room1", 0)
            .AddOccupation(new Occupation("Partner1", "PartnerPatient", "*PARTNER", "long", new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0)))
            .AddOccupation(new Occupation("Partner2", "PartnerPatient", "*PARTNER", "long", new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0)));
        var expectedRoom2 = new Room("room2", 1).AddOccupation(new Occupation("Dani", "Carmen", "short2", "long2", new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0)));

        var resultRooms = _roomsOccupationsFiller.FillRoomOccupations(rows, new[] { "room1", "room2" }, 10).ToList();

        resultRooms[0].Occupations.Should().BeEquivalentTo(expectedRoom1.Occupations);
        resultRooms[1].Occupations.Should().BeEquivalentTo(expectedRoom2.Occupations);
    }
}
