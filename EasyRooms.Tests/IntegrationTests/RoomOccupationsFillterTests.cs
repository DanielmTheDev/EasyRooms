using EasyRooms.Implementations;
using EasyRooms.Models;
using FluentAssertions;
using System;
using Xunit;

namespace EasyRooms.Tests.IntegrationTests
{
    public class RoomOccupationsFillterTests
    {
        private readonly RoomOccupationsFiller _roomsManager;

        public RoomOccupationsFillterTests()
            => _roomsManager = new RoomOccupationsFiller();

        [Fact]
        public void Distrubutes_Overlapping_Rows_Between_Two_Rooms()
        {
            var rows = new[]
            {
                new Row("08:00", "4", "short", "long", "Hans", "Wadim"),
                new Row("10:00", "4", "short2", "long2", "Carmen", "Dani"),
            };
            var room1 = new Room("room1").AddOccupation(new Occupation("Wadim", "Hans", "short", "long", new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0)));
            var room2 = new Room("room2").AddOccupation(new Occupation("Dani", "Carmen", "short", "long", new TimeSpan(10, 0, 0), new TimeSpan(14, 0, 0)));
            var expectedRooms = new[] { room1, room2 };

            var resultRooms = RoomOccupationsFiller.FillRoomOccupations(rows, new[] { "room1", "room2" });

            resultRooms.Should().BeEquivalentTo(expectedRooms);
        }
    }
}
