using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Interfaces;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Implementations;

public class PartnerRoomFiller : IPartnerRoomFiller
{
    private readonly IOccupationKeyInformationExtractor _occupationKeyInformationExtractor;

    public PartnerRoomFiller(IOccupationKeyInformationExtractor occupationKeyInformationExtractor) 
        => _occupationKeyInformationExtractor = occupationKeyInformationExtractor;

    public void AddPartnerTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
    {
        var partnerTherapies = orderedRows
            .Where(row => string.Equals(row.TherapyShort, "*partner", StringComparison.OrdinalIgnoreCase))
            .GroupBy(row => (row.StartTime, row.Duration))
            .ToList();
        partnerTherapies.ForEach(grouping =>
        {
            var (startTime, endTime, freeRoom) = _occupationKeyInformationExtractor.GetOccupationInformation(grouping.Key.StartTime, grouping.Key.Duration, bufferInMinutes, rooms);
            grouping.ToList().ForEach(row => freeRoom.AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong, startTime, endTime)));
            grouping.ToList().ForEach(row => orderedRows.Remove(row));
        });
    }
}
