namespace EasyRooms.Model.Therapies.Implementations;

internal class PreparationsAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public PreparationsAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IEnumerable<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var preparations = orderedRows
            .Where(row => TherapyTypeProvider.IsPreparation(row.TherapyShort))
            .ToList();

        preparations.ForEach(preparation =>
        {
            _occupationsAdder.AddToSpecificRoom(rooms, string.Empty, preparation);
            orderedRows.Remove(preparation);
        });
    }
}