using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Constants;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Therapy
{
    public class TherapyFiller : ITherapyFiller
    {
        private readonly IFreeRoomFinder _freeRoomFinder;

        public TherapyFiller(IFreeRoomFinder freeRoomFinder)
            => _freeRoomFinder = freeRoomFinder;

        public void AddAllTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
        {
            AddPartnerTherapies(rooms, orderedRows, bufferInMinutes);
            AddRoomSpecificMassages(rooms, orderedRows, bufferInMinutes, roomNames);
            AddNormalTherapies(rooms, orderedRows, bufferInMinutes);
        }

        private void AddPartnerTherapies(IEnumerable<Room> rooms, ICollection<Row> orderedRows, int bufferInMinutes)
        {
            var partnerTherapies = orderedRows
                .Where(row => string.Equals(row.TherapyShort, CommonConstants.PartnerString, StringComparison.OrdinalIgnoreCase))
                .GroupBy(row => (row.StartTime, row.Duration))
                .ToList();

            partnerTherapies.ForEach(grouping =>
            {
                AddOccupation(rooms, bufferInMinutes, grouping.ToArray());
                grouping.ToList().ForEach(row => orderedRows.Remove(row));
            });
        }

        private void AddRoomSpecificMassages(IEnumerable<Room> rooms, ICollection<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
        {
            var roomSpecificMassages = orderedRows
                .Where(row => roomNames.MassagesForSpecificRoomsAsList.Contains(row.TherapyShort))
                .GroupBy(row => (row.StartTime, row.Duration))
                .ToList();

            var specificRooms = rooms.Where(room => room.IsMassageSpecificRoom);

            roomSpecificMassages.ForEach(grouping =>
            {
                grouping.ToList().ForEach(row => AddOccupation(specificRooms, bufferInMinutes, row));
                grouping.ToList().ForEach(row => orderedRows.Remove(row));
            });
        }

        private void AddNormalTherapies(IReadOnlyCollection<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
            => orderedRows.ForEach(row => AddOccupation(rooms, bufferInMinutes, row));

        private void AddOccupation(IEnumerable<Room> rooms, int bufferInMinutes, params Row[] rows)
        {
            var (startTime, timeSpan, freeRoom) = _freeRoomFinder.FindFreeRoom(rows.First().StartTime, rows.First().Duration, bufferInMinutes, rooms);
            foreach (var row in rows)
            {
                freeRoom.AddOccupation(new Occupation(row, startTime, timeSpan));
            }
        }
    }
}