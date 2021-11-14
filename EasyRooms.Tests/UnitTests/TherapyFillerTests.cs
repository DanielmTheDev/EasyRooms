// using System;
// using System.Collections.Generic;
// using EasyRooms.Model.Constants;
// using EasyRooms.Model.Rooms;
// using EasyRooms.Model.Rooms.Models;
// using EasyRooms.Model.Rows.Models;
// using EasyRooms.Model.Therapy;
// using NSubstitute;
// using Xunit;
//
// namespace EasyRooms.Tests.UnitTests
// {
//     public class TherapyFillerTests
//     {
//         private readonly TherapyFiller _therapyFiller;
//         private readonly IFreeRoomFinder _freeRoomFinder;
//
//         public TherapyFillerTests()
//         {
//             _freeRoomFinder = Substitute.For<IFreeRoomFinder>();
//             _therapyFiller = new TherapyFiller(_freeRoomFinder);
//         }
//
//         //Continue here: manual test, unit test and then refactoring this
//         [Fact]
//         public void Adds_Therapies()
//         {
//             var roomNames = CreateRoomNames();
//             var rows = CreateRows();
//             var rooms = CreateRooms();
//             var expectedRooms = CreateExpectedRooms();
//
//             _therapyFiller.AddAllTherapies(rooms, rows, 0, roomNames);
//             
//         }
//
//         private static List<Room> CreateRooms()
//             => new()
//             {
//                 new Room("normalRoom", 0), 
//                 new Room("partnerRoom", 1), 
//                 new Room("massageSpecificRoom", 2)
//             };
//
//         private static List<Row> CreateRows()
//             => new()
//             {
//                 new Row("08:00", "2", "someOtherTherapy", "long", "patient", "therapist"),
//                 new Row("11:00", "2", "someOtherTherapy", "long", "patient", "therapist"),
//                 new Row("08:00", "2", CommonConstants.PartnerString, "long", "patient", "therapist"),
//                 new Row("11:00", "2", CommonConstants.PartnerString, "long", "patient", "therapist"),
//                 new Row("08:00", "2", "roomSpecificMassage", "roomSpecificMassage", "patient2", "therapist2"),
//                 new Row("011:00", "2", "roomSpecificMassage", "roomSpecificMassage", "patient2", "therapist2")
//             };
//
//
//         private IEnumerable<Room> CreateExpectedRooms()
//             => new List<Room>
//             {
//                 new Room("normalRoom", 0)
//                     .AddOccupation(new Occupation("therapist1", "patient1", "therapyShort1", "therapyLong1",
//                         new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0)))
//                     .AddOccupation(new Occupation("therapist1", "patient1", "therapyShort1", "therapyLong1",
//                         new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0)))
//             };
//
//         private static RoomNames CreateRoomNames()
//             => new("normalRoom\npartnerRoom\nmassageSpecificRoom", "partnerRoom", "massageSpecificRoom",
//                 "roomSpecificMassage");
//     }
// }