namespace EasyRooms.Model.Therapies.Implementations;

// distributes rows into free rooms.
// If there are rows whose start time is adjacent and which have the same patient, they will be put into the same room
public class AdjacentTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public AdjacentTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        while (orderedRows.Count > 0)
        {
            var connectedRows = GetAdjacentRowsWithSamePatient(orderedRows);
            _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, connectedRows.ToArray());
            orderedRows.RemoveAll(row => connectedRows.Contains(row));
        }
    }

    private static IReadOnlyCollection<Row> GetAdjacentRowsWithSamePatient(IReadOnlyList<Row> orderedRows)
        => orderedRows
            .Where(row => row.Patient == orderedRows[0].Patient)
            .OrderBy(row => row.StartTime)
            .Aggregate(new List<Row> { orderedRows[0] }, (acc, curr) =>
                curr.StartTimeAsTimeSpan == acc.Last().StartTimeAsTimeSpan + acc.Last().DurationAsTimeSpan
                    ? acc.Concat(new[] { curr }).ToList()
                    : acc);
}