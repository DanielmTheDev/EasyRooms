using EasyRooms.Model.TimeWindow.Interfaces;

namespace EasyRooms.Model.Therapies.Implementations;

public class TimeSpecificTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;
    private readonly ITimeWindowValueHolder _times;

    public TimeSpecificTherapiesAdder(IOccupationsAdder occupationsAdder, ITimeWindowValueHolder times)
    {
        _occupationsAdder = occupationsAdder;
        _times = times;
    }

    public void Add(IEnumerable<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var timeSpecificTherapies = orderedRows
            .Where(row => TherapyTypeProvider.IsTimeSpecificTherapy(row.StartTimeAsTimeOnly, row.EndTimeAsTimeOnly, _times.StartTimeAsTimeOnly, _times.EndTimeAsTimeOnly))
            .ToList();

        var specificRooms = rooms.Where(room => room.IsMassageSpecificRoom);

        timeSpecificTherapies.ForEach(therapy =>
        {
            _occupationsAdder.AddToFreeRoom(specificRooms, bufferInMinutes, therapy);
            orderedRows.Remove(therapy);
        });
    }
}