using EasyRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Implementations
{
    public class RoomOccupationsFiller
    {
        public static IEnumerable<Room> FillRoomOccupations(IEnumerable<Row> rows, IEnumerable<string> roomNames)
        {
            var orderedRows = OrderRows(rows);
            var rooms = CreateRooms(roomNames, orderedRows);
        }

        private static IEnumerable<Room> CreateRooms(IEnumerable<string> roomNames, IOrderedEnumerable<Row> orderedRows)
        {
            var rooms = roomNames.Select((name, i) => new Room(name, i));
            orderedRows.ToList()
                .ForEach(row =>
                {

                    rooms.First(room => room.IsEmptyAt(row.StartTime, row.Duration))
                        //todo create occupation constructor taking row
                        //todo make row properties better types than string. Consider trimming (12:12) paranthesis for some start times when parsing
                        .AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong, row.StartTime, row.StartTime.AddHours() ))
                });
        }

        //todo validate duation is int, maybe earlier when parsing the document
        private static IOrderedEnumerable<Row> OrderRows(IEnumerable<Row> rows) =>
            rows
                .OrderBy(row => DateTime.Parse(row.StartTime))
                .ThenBy(row => int.Parse(row.Duration));
    }
}
