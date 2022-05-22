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
        var lookedAtAllRows = false;
        var i = 0;
        while (!lookedAtAllRows)
        {
            var connectedRows = GetAdjacentRowsWithSamePatient(orderedRows, i);
            _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, connectedRows.ToArray());
            orderedRows.RemoveAll(row => connectedRows.Contains(row));
            if (++i >= orderedRows.Count)
                lookedAtAllRows = true;
        }
    }

    private static IReadOnlyCollection<Row> GetAdjacentRowsWithSamePatient(IReadOnlyList<Row> orderedRows, int i)
        => orderedRows
            .Where(row => row.Patient == orderedRows[i].Patient)
            .OrderBy(row => row.StartTime)
            .Aggregate(new List<Row> { orderedRows[i] }, (acc, curr) =>
                curr.StartTimeAsTimeSpan == acc.Last().StartTimeAsTimeSpan + acc.Last().DurationAsTimeSpan
                    ? acc.Concat(new[] { curr }).ToList()
                    : acc);
}