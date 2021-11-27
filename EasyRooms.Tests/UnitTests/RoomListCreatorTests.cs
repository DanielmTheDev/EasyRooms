using System.Collections.Generic;
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
            var roomNames = new RoomNames(roomString, partnerString, "room1");
            var expectedRooms = CreateExpectedPartnerRooms();
            
            var rooms = _roomListCreator.CreateRooms(roomNames);
            
            rooms.Should().BeEquivalentTo(expectedRooms, config => config.ComparingByMembers<Room>());
        }
        
        [Fact]
        public void Creates_Room_With_MassageSpecificRoom_Properties()
        {
            const string roomString = "room1\nroom2\nroom3";
            const string roomSpecificRooms = "room2\nroom3";
            var roomNames = new RoomNames(roomString, "room1", roomSpecificRooms);
            var expectedRooms = CreateExpectedMassageSpecificRooms();
            
            var rooms = _roomListCreator.CreateRooms(roomNames);
            
            rooms.Should().BeEquivalentTo(expectedRooms, config => config.ComparingByMembers<Room>());
        }

        private IEnumerable<Room> CreateExpectedMassageSpecificRooms()
        {
            var expectedRooms = new List<Room>
            {
                new("room1", 0),
                new("room2", 1),
                new("room3", 2),
            };
            expectedRooms[0].IsPartnerRoom = true;
            expectedRooms[1].IsMassageSpecificRoom = true;
            expectedRooms[2].IsMassageSpecificRoom = true;
            return expectedRooms;
        }

        private static IEnumerable<Room> CreateExpectedPartnerRooms()
        {
            var expectedRooms = new List<Room>
            {
                new("room1", 0),
                new("room2", 1),
                new("room3", 2),
            };
            expectedRooms[0].IsMassageSpecificRoom = true;
            expectedRooms[1].IsPartnerRoom = true;
            expectedRooms[2].IsPartnerRoom = true;
            return expectedRooms;
        }
    }
}