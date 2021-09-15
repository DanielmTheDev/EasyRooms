using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Interfaces;
using EasyRooms.Model.Models;


namespace EasyRooms.Model.Implementations;

public class RoomOccupationsFiller : IRoomOccupationsFiller
{
    public IEnumerable<Room> FillRoomOccupations(IEnumerable<Row> rows, IEnumerable<string> roomNames, int bufferInMinutes = 0)
    {
        var orderedRows = OrderRows(rows).ToList();
        return CreateRooms(roomNames, orderedRows, bufferInMinutes);
    }

    private static IEnumerable<Room> CreateRooms(IEnumerable<string> roomNames, List<Row> orderedRows, int bufferInMinutes)
    {
        var rooms = roomNames.Select((name, i) => new Room(name, i)).ToList();
        AddPartnerTherapies(rooms, orderedRows, bufferInMinutes);
        AddNormalTherapies(rooms, orderedRows, bufferInMinutes);
        return rooms;
    }

    private static void AddPartnerTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
    {
        var partnerTherapies = orderedRows
            .Where(row => string.Equals(row.TherapyShort, "*partner", StringComparison.OrdinalIgnoreCase))
            .GroupBy(row => new { row.StartTime, row.Duration })
            .ToList();
        partnerTherapies.ForEach(grouping =>
        {
            var (startTime, endTime, freeRoom) = GetOccupationInformation(grouping.Key.StartTime, grouping.Key.Duration, bufferInMinutes, rooms);
            grouping.ToList().ForEach(row => freeRoom.AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong, startTime, endTime)));
            grouping.ToList().ForEach(row => orderedRows.Remove(row));
        });
    }

    private static void AddNormalTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes) 
        => orderedRows.ForEach(row => AddOccupation(row, rooms, bufferInMinutes));

    private static void AddOccupation(Row row, List<Room> rooms, int bufferInMinutes)
    {
        var (startTime, endTime, freeRoom) = GetOccupationInformation(row.StartTime, row.Duration, bufferInMinutes, rooms);
        freeRoom.AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong, startTime, endTime));
    }

    private static (TimeSpan startTime, TimeSpan endTime, Room freeRoom) GetOccupationInformation(string startTimeString, string duration, int bufferInMinutes, List<Room> rooms)
    {
        //todo this trimming is a workaround. In reality, such a case probably has to be put into the same room as the
        //theray that came before, since it means something like preparation
        var startTime = TimeSpan.Parse(startTimeString.Trim('(', ')'));
        var endTime = AddDurationAsMinutes(duration, startTime);
        var freeRoom = rooms.First(room => !room.IsOccupiedAt(startTime, endTime, bufferInMinutes));
        return (startTime, endTime, freeRoom);
    }

    private static TimeSpan AddDurationAsMinutes(string duration, TimeSpan startTime)
    {
        var minutes = TimeSpan.FromMinutes(int.Parse(duration));
        return startTime.Add(minutes);
    }

    private static IOrderedEnumerable<Row> OrderRows(IEnumerable<Row> rows) 
        => rows.OrderBy(row => TimeSpan.Parse(row.StartTime.Trim('(', ')')))
            .ThenBy(row => int.Parse(row.Duration));
}
