using System;
using EasyRooms.Model.Rooms.Models;
using FluentAssertions;
using Xunit;

namespace EasyRooms.Tests.UnitTests;

public class RoomNamesTests
{
    [Fact]
    public void Throws_If_PartnerRoom_Is_Not_In_Regular_Room()
    {
        Func<RoomNames> newRoomNames = () => new RoomNames("room1\nroom2", "partnerRoom", "room1");

        newRoomNames.Should().Throw<ArgumentException>().WithMessage("PartnerRoomString");
    }
        
    [Fact]
    public void Throws_If_SpecificRoom_Is_Not_In_Regular_Room()
    {
        Func<RoomNames> newRoomNames = () => new RoomNames("room1\nroom2", "room1", "wrong");

        newRoomNames.Should().Throw<ArgumentException>().WithMessage("RoomsForSpecificMassages");
    }
}