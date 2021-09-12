using EasyRooms.Interfaces;
using EasyRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Implementations
{
    public class RoomOccupationsFiller : IRoomOccupationsFiller
    {
        public IEnumerable<Room> FillRoomOccupations(IEnumerable<Row> rows, IEnumerable<string> roomNames, int bufferInMinutes = 0)
        {
            //todo partner massage
            //1. find all rows with *partner as therapy and pair them up
            //2. stick them into the prioritized room (either passing one or taking the highest)
            //3. remove those rows from from the total ones
            //4. start filling with normal algorithm
            var orderedRows = OrderRows(rows);
            return CreateRooms(roomNames, orderedRows, bufferInMinutes);
        }

        private static IEnumerable<Room> CreateRooms(IEnumerable<string> roomNames, IOrderedEnumerable<Row> orderedRows, int bufferInMinutes)
        {
            var rooms = roomNames.Select((name, i) => new Room(name, i)).ToList();
            orderedRows.ToList().ForEach(row => AddOccupation(row, rooms, bufferInMinutes));
            return rooms;
        }

        private static void AddOccupation(Row row, List<Room> rooms, int bufferInMinutes)
        {
            //todo this trimming is a workaround. In reality, such a case probably has to be put into the same room as the
            //theray that came before, since it means something like preparation
            var startTime = TimeSpan.Parse(row.StartTime.Trim('(', ')'));
            var endTime = AddDurationAsMinutes(row.Duration, startTime);
            rooms.First(room => !room.IsOccupiedAt(startTime, endTime, bufferInMinutes))
                .AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong, startTime, endTime));
        }

        private static TimeSpan AddDurationAsMinutes(string duration, TimeSpan startTime)
        {
            var minutes = TimeSpan.FromMinutes(int.Parse(duration));
            return startTime.Add(minutes);
        }

        private static IOrderedEnumerable<Row> OrderRows(IEnumerable<Row> rows) =>
            rows.OrderBy(row => TimeSpan.Parse(row.StartTime.Trim('(', ')')))
                .ThenBy(row => int.Parse(row.Duration));
    }
}
