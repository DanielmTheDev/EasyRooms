using EasyRooms.Model.Comparison.Implementations;
using EasyRooms.Model.Therapies.Models;

namespace EasyRooms.Model.Therapies.Implementations;

public class PartnerTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;
    private readonly ITherapyTypeComparer _comparer;

    public PartnerTherapiesAdder(IOccupationsAdder occupationsAdder, ITherapyTypeComparer comparer)
    {
        _occupationsAdder = occupationsAdder;
        _comparer = comparer;
    }

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var groupedPartnerTherapies = GroupByTime(orderedRows, row => _comparer.IsPartnerTherapy(row.TherapyShort));
        var groupedAfterTherapies = GroupByTime(orderedRows, row => _comparer.IsAfterTherapy(row.TherapyShort));

        groupedPartnerTherapies.ForEach(partnerGroup =>
        {
            var allRows = GetAllRowsToAdd(partnerGroup, groupedAfterTherapies);
            var partnerRooms = rooms.Where(room => room.IsPartnerRoom);
            _occupationsAdder.AddToFreeRoom(partnerRooms, bufferInMinutes, allRows.ToArray());
            allRows.ForEach(row => orderedRows.Remove(row));
        });
    }

    private static List<Row> GetAllRowsToAdd(IGrouping<StartTimeWithDuration, Row> partnerGroup, IEnumerable<IGrouping<StartTimeWithDuration, Row>> groupedAfterTherapies)
    {
        var matchingAfterTherapies = GetMatchingAfterTherapies(partnerGroup.First().EndTime, groupedAfterTherapies.ToList());
        return partnerGroup
            .Concat(matchingAfterTherapies)
            .ToList();
    }

    private static IEnumerable<Row> GetMatchingAfterTherapies(string endTime, IEnumerable<IGrouping<StartTimeWithDuration, Row>> groupedAfterTherapies)
        => groupedAfterTherapies
            .SingleOrDefault(rows => rows.Key.StartTime == endTime)
            ?.ToList() ?? Enumerable.Empty<Row>();

    private static List<IGrouping<StartTimeWithDuration, Row>> GroupByTime(IEnumerable<Row> orderedRows, Predicate<Row> predicate)
        => orderedRows
            .Where(row => predicate(row))
            .GroupBy(row => new StartTimeWithDuration(row.StartTime, row.Duration))
            .ToList();
}