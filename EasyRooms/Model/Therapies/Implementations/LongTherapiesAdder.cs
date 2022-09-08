using EasyRooms.Model.Comparison.Implementations;

namespace EasyRooms.Model.Therapies.Implementations;

public class LongTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public LongTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var longTherapyRows = orderedRows
            .Where(row => TherapyTypeComparer.IsLongTherapy(row.DurationAsTimeSpan))
            .ToList();

        longTherapyRows.ForEach(therapy =>
        {
            _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, therapy);
            orderedRows.Remove(therapy);
        });
    }
}