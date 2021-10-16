using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Interfaces;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Implementations;

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

    private void AddPartnerTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
    {
        var partnerTherapies = orderedRows
            .Where(row => string.Equals(row.TherapyShort, "*partner", StringComparison.OrdinalIgnoreCase))
            .GroupBy(row => (row.StartTime, row.Duration))
            .ToList();

        partnerTherapies.ForEach(grouping =>
        {
            var freeRoom = _freeRoomFinder.CalculateOccupationCreationData(grouping.Key.StartTime,
                grouping.Key.Duration, bufferInMinutes, rooms);
            //todo add occupation constructor that takes row
            grouping.ToList()
                .ForEach(row => freeRoom.FreeRoom
                    .AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong,
                        freeRoom.StartTime, freeRoom.EndTime)));
            grouping.ToList()
                .ForEach(row => orderedRows.Remove(row));
        });
    }

    private void AddNormalTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
        => orderedRows.ForEach(row => AddOccupation(row, rooms, bufferInMinutes));

    private void AddOccupation(Row row, List<Room> rooms, int bufferInMinutes)
    {
        var occupationCreationData = _freeRoomFinder
            .CalculateOccupationCreationData(row.StartTime, row.Duration, bufferInMinutes, rooms);
        occupationCreationData.FreeRoom
            .AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong,
                occupationCreationData.StartTime, occupationCreationData.EndTime));
    }
}