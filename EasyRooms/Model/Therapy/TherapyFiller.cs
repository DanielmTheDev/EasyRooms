using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Constants;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Therapy;

public class TherapyFiller : ITherapyFiller
{
    private readonly IFreeRoomFinder _freeRoomFinder;

    public TherapyFiller(IFreeRoomFinder freeRoomFinder)
        => _freeRoomFinder = freeRoomFinder;

    public void AddAllTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
    {
        AddPartnerTherapies(rooms, orderedRows, bufferInMinutes);
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
            grouping.ToList().ForEach(row => AddOccupation(row, rooms, bufferInMinutes));
            grouping.ToList().ForEach(row => orderedRows.Remove(row));
        });
    }

    private void AddNormalTherapies(IReadOnlyCollection<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
        => orderedRows.ForEach(row => AddOccupation(row, rooms, bufferInMinutes));

    private void AddOccupation(Row row, IEnumerable<Room> rooms, int bufferInMinutes)
    {
        var freeRoom = _freeRoomFinder.CalculateOccupationCreationData(row.StartTime, row.Duration, bufferInMinutes, rooms);
        freeRoom.FreeRoom.AddOccupation(new Occupation(row, freeRoom.StartTime, freeRoom.EndTime));
    }
}