using System.Collections.Generic;
using System.IO;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Validation;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace EasyRooms.Tests.IntegrationTests
{
    public class RoomsValidatorTests
    {
        private readonly RoomsValidator _validator;

        public RoomsValidatorTests() 
            => _validator = new RoomsValidator();

        [Fact]
        public void Validates_Partner_Massages()
        {
            var serializedRooms = File.ReadAllText("./IntegrationTests/TestData/validationInputRooms.json");
            var defaultRoomNames = new RoomNames();
            var rooms = JsonConvert.DeserializeObject<IReadOnlyCollection<Room>>(serializedRooms);

            var validationResult = _validator.IsValid(rooms!, defaultRoomNames);

            validationResult.Should().BeTrue();
        }
    }
}