using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Comparison.Implementations;

namespace EasyRooms.Model.Therapies.Implementations;

internal class PreparationsAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;
    private readonly ITherapyTypeComparer _comparer;

    public PreparationsAdder(IOccupationsAdder occupationsAdder, ITherapyTypeComparer comparer)
    {
        _occupationsAdder = occupationsAdder;
        _comparer = comparer;
    }

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        orderedRows
            .Where(row => _comparer.IsPreparation(row.TherapyShort))
            .ForEach(preparation =>
            {
                _occupationsAdder.AddToSpecificRoom(rooms, string.Empty, preparation);
                orderedRows.Remove(preparation);
            });
    }
}