using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Occupations;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Validation;

namespace EasyRooms.Model.Therapies.PartnerTherapies;

public class PartnerTherapiesAdder : IPartnerTherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public PartnerTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void AddPartnerTherapies(IEnumerable<Room> rooms, ICollection<Row> orderedRows, int bufferInMinutes)
    {
        var partnerTherapies = GetPartnerTherapies(orderedRows);

        partnerTherapies.ForEach(grouping =>
        {
            _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, grouping.ToArray());
            grouping.ToList().ForEach(row => orderedRows.Remove(row));
        });
    }

    private static List<IGrouping<(string StartTime, string Duration), Row>> GetPartnerTherapies(IEnumerable<Row> orderedRows)
        => orderedRows
            .Where(row => TherapyTypeProvider.IsPartnerTherapy(row.TherapyShort))
            .GroupBy(row => (row.StartTime, row.Duration))
            .ToList();
}