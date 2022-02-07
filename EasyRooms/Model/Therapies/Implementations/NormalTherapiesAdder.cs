namespace EasyRooms.Model.Therapies.Implementations;

public class NormalTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public NormalTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IEnumerable<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
        => orderedRows.ForEach(row => _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, row));
}