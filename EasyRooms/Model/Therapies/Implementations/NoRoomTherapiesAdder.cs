using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Constants;

namespace EasyRooms.Model.Therapies.Implementations;

public class NoRoomTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public NoRoomTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var noRoomTherapies = orderedRows
            .Where(row =>
                row.Comment.EqualsInvariant(CommonConstants.HomeVisit)
                || row.TherapyShort.ContainsInvariant(CommonConstants.Pause)
                || (row.TherapyShort is "" && row.TherapyLong is "" && row.Patient is ""));
        noRoomTherapies.ForEach(row =>
        {
            _occupationsAdder.AddToSpecificRoom(rooms, string.Empty, row);
            orderedRows.Remove(row);
        });
    }
}