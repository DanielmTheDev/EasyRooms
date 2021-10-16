using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Interfaces;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Implementations;

public class TherapyFiller : ITherapyFiller
{
    private readonly IOccupationCreationDataProvider _occupationCreationDataProvider;

    public TherapyFiller(IOccupationCreationDataProvider occupationCreationDataProvider) 
        => _occupationCreationDataProvider = occupationCreationDataProvider;

    public void AddPartnerTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
    {
        var partnerTherapies = orderedRows
            .Where(row => string.Equals(row.TherapyShort, "*partner", StringComparison.OrdinalIgnoreCase))
            .GroupBy(row => (row.StartTime, row.Duration))
            .ToList();
        partnerTherapies.ForEach(grouping =>
        {
            var occupationCreationData = _occupationCreationDataProvider.CalculateOccupationCreationData(grouping.Key.StartTime, grouping.Key.Duration, bufferInMinutes, rooms);
            //todo add occupation constructor that takes row
            grouping.ToList()
                .ForEach(row => occupationCreationData.FreeRoom
                    .AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong, occupationCreationData.StartTime, occupationCreationData.EndTime)));
            grouping.ToList()
                .ForEach(row => orderedRows.Remove(row));
        });
    }

    public void AddNormalTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
        => orderedRows.ForEach(row => AddOccupation(row, rooms, bufferInMinutes));

    private void AddOccupation(Row row, List<Room> rooms, int bufferInMinutes)
    {
        var occupationCreationData = _occupationCreationDataProvider
            .CalculateOccupationCreationData(row.StartTime, row.Duration, bufferInMinutes, rooms);
        occupationCreationData.FreeRoom
            .AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong,
                occupationCreationData.StartTime, occupationCreationData.EndTime));
    }
}
