using EasyRooms.Model.CommonExtensions;

namespace EasyRooms.Model.Therapies.Implementations;

internal class PreparationsAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public PreparationsAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        orderedRows
            .Where(row => TherapyTypeComparer.IsPreparation(row.TherapyShort))
            .ForEach(preparation =>
            {
                _occupationsAdder.AddToSpecificRoom(rooms, string.Empty, preparation);
                orderedRows.Remove(preparation);
            });
    }
}