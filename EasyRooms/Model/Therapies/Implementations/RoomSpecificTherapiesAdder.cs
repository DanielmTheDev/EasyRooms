namespace EasyRooms.Model.Therapies.Implementations;

// items contained in the specific therapies list are distributed before the others, giving them priority
public class RoomSpecificTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public RoomSpecificTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var roomSpecificMassages = orderedRows
            .Where(row => roomNames.MassagesForSpecificRoomsAsList.Contains(row.TherapyShort))
            .GroupBy(row => (row.StartTime, row.Duration))
            .ToList();

        roomSpecificMassages.ForEach(grouping =>
        {
            grouping.ToList().ForEach(row => _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, row));
            grouping.ToList().ForEach(row => orderedRows.Remove(row));
        });
    }
}