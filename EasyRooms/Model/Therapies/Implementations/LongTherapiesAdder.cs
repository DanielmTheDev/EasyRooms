using EasyRooms.Model.Comparison.Implementations;

namespace EasyRooms.Model.Therapies.Implementations;

public class LongTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;
    private readonly ITherapyTypeComparer _comparer;

    public LongTherapiesAdder(IOccupationsAdder occupationsAdder, ITherapyTypeComparer comparer)
    {
        _occupationsAdder = occupationsAdder;
        _comparer = comparer;
    }

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var longTherapyRows = orderedRows
            .Where(row => _comparer.IsLongTherapy(row.DurationAsTimeSpan))
            .ToList();

        longTherapyRows.ForEach(therapy =>
        {
            _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, therapy);
            orderedRows.Remove(therapy);
        });
    }
}