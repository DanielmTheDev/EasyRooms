namespace EasyRooms.Model.Therapies.Implementations;

// distributes rows into free rooms.
// If there are rows whose start time is adjacent and which have the same patient, they will be put into the same room
public class AdjacentTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;
    private readonly IAdjacentTherapiesExtractor _adjacentTherapiesExtractor;

    public AdjacentTherapiesAdder(IOccupationsAdder occupationsAdder, IAdjacentTherapiesExtractor adjacentTherapiesExtractor)
    {
        _occupationsAdder = occupationsAdder;
        _adjacentTherapiesExtractor = adjacentTherapiesExtractor;
    }

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        while (orderedRows.Count > 0)
        {
            var connectedRows = _adjacentTherapiesExtractor.GetAdjacentRowsWithSamePatient(orderedRows, orderedRows[0]);
            _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, connectedRows.ToArray());
            orderedRows.RemoveAll(row => connectedRows.Contains(row));
        }
    }
}