namespace EasyRooms.Model.Therapies.Implementations;

public class TherapyFiller : ITherapyFiller
{
    private readonly IEnumerable<ITherapiesAdder> _therapyAdders;

    public TherapyFiller(IEnumerable<ITherapiesAdder> therapyAdders)
        => _therapyAdders = therapyAdders;

    public void AddAllTherapies(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        foreach (var therapiesAdder in _therapyAdders)
        {
            therapiesAdder.Add(rooms, orderedRows, bufferInMinutes, roomNames);
        }
    }
}