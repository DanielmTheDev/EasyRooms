using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Occupations;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapies.PartnerTherapies.Models;
using EasyRooms.Model.Validation;

namespace EasyRooms.Model.Therapies.PartnerTherapies;

public class PartnerTherapiesAdder : IPartnerTherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public PartnerTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void AddPartnerTherapies(IEnumerable<Room> rooms, ICollection<Row> orderedRows, int bufferInMinutes)
    {
        var groupedPartnerTherapies = GroupByTime(orderedRows, row => TherapyTypeProvider.IsPartnerTherapy(row.TherapyShort));
        var groupedAfterTherapies = GroupByTime(orderedRows, row => TherapyTypeProvider.IsAfterTherapy(row.TherapyShort));

        groupedPartnerTherapies.ForEach(partnerGroup =>
        {
            var allRows = GetAllRowsToAdd(partnerGroup, groupedAfterTherapies);
            _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, allRows.ToArray());
            allRows.ForEach(row => orderedRows.Remove(row));
        });
    }

    private static List<Row> GetAllRowsToAdd(IGrouping<StartTimeWithDuration, Row> partnerGroup, IEnumerable<IGrouping<StartTimeWithDuration, Row>> groupedAfterTherapies)
    {
        var matchingAfterTherapies = GetMatchingAfterTherapies(partnerGroup.First().EndTime, groupedAfterTherapies.ToList());
        var allRows = partnerGroup.Concat(matchingAfterTherapies).ToList();
        return allRows;
    }

    private static IEnumerable<Row> GetMatchingAfterTherapies(string endTime, IEnumerable<IGrouping<StartTimeWithDuration, Row>> groupedAfterTherapies)
        => groupedAfterTherapies
            .Single(rows => rows.Key.StartTime == endTime)
            .ToList();

    private static List<IGrouping<StartTimeWithDuration, Row>> GroupByTime(IEnumerable<Row> orderedRows, Predicate<Row> predicate)
        => orderedRows
            .Where(row => predicate(row))
            .GroupBy(row => new StartTimeWithDuration(row.StartTime, row.Duration))
            .ToList();
}