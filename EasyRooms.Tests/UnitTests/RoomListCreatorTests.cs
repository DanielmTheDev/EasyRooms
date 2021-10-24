using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Models;
using FluentAssertions;
using Xunit;

namespace EasyRooms.Tests.UnitTests
{
    public class RoomListCreatorTests
    {
        private readonly RoomListCreator _roomListCreator;

        public RoomListCreatorTests() 
            => _roomListCreator = new RoomListCreator();

        [Fact]
        public void Creates_Room_With_Partner_Properties()
        {
            const string roomString = "room1\nroom2\nroom3";
            const string partnerString = "room2\nroom3";
            var roomNames = new RoomNames(roomString, partnerString);
            var expectedRooms = CreateExpectedRooms().ToList();
            
            var rooms = _roomListCreator.CreateRooms(roomNames);
            
            rooms.Should().BeEquivalentTo(expectedRooms, config => config.ComparingByMembers<Room>());
        }

        private static IEnumerable<Room> CreateExpectedRooms()
        {
            var expectedRooms = new List<Room>
            {
                new("room1", 0),
                new("room2", 1),
                new("room3", 2),
            };
            expectedRooms[1].IsPartnerRoom = true;
            expectedRooms[2].IsPartnerRoom = true;
            return expectedRooms;
        }
    }
}