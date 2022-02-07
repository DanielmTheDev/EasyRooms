namespace EasyRooms.Model.Therapies.Implementations;

public class TherapyFiller : ITherapyFiller
{
    private readonly IEnumerable<ITherapiesAdder> _massageAdders;

    public TherapyFiller(IEnumerable<ITherapiesAdder> massageAdders)
        => _massageAdders = massageAdders;

    public void AddAllTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        foreach (var massageAdder in _massageAdders)
        {
            massageAdder.Add(rooms, orderedRows, bufferInMinutes, roomNames);
        }
    }
}