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
            return CreateRooms(roomNames, orderedRows);
        }

        private static IEnumerable<Room> CreateRooms(IEnumerable<string> roomNames, IOrderedEnumerable<Row> orderedRows)
        {
            var rooms = roomNames.Select((name, i) => new Room(name, i));
            orderedRows.ToList()
                .ForEach(row =>
                {
                    var startTimeSpan = TimeSpan.Parse(row.StartTime);
                    var endTimeSpan = startTimeSpan.Add(TimeSpan.Parse(row.Duration));
                    rooms.First(room => !room.IsOccupiedAt(startTimeSpan, endTimeSpan))
                        .AddOccupation(new Occupation(row));
                });
            return rooms;
        }

        //todo validate duation is int, maybe earlier when parsing the document
        private static IOrderedEnumerable<Row> OrderRows(IEnumerable<Row> rows) =>
            rows
                .OrderBy(row => DateTime.Parse(row.StartTime))
                .ThenBy(row => int.Parse(row.Duration));
    }
}
