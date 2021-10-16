using System;
using System.Collections.Generic;
using EasyRooms.Model.Implementations;
using EasyRooms.Model.Models;
using FluentAssertions;
using Xunit;

namespace EasyRooms.Tests.UnitTests
{
    public class OccupationCreationDataProviderTests
    {
        private FreeRoomFinder _provider;

        public OccupationCreationDataProviderTests() 
            => _provider = new FreeRoomFinder();

        [Fact]
        public void Calculates_Occupation_Data_With_Free_Room()
        {
            var room1 = new Room("room1", 0)
                .AddOccupation(new Occupation("Wadim", "Hans", "short", "long", new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0)));
            var room2 = new Room("room2", 1);
            var rooms = new List<Room> { room1, room2 };
            var expectedOccupationData =
                new FreeRoomWithTime(new TimeSpan(9, 0, 0), new TimeSpan(11, 0, 0), room2);

            var occupationData = _provider.CalculateOccupationCreationData("09:00", "120", 5, rooms);

            occupationData.Should().BeEquivalentTo(expectedOccupationData);
        }
    }
}
