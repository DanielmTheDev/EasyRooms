using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Constants;

namespace EasyRooms.Model.Therapies.Implementations;

//adds home visits to the empty room
public class HomeVisitAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public HomeVisitAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var homeVisitRows = orderedRows
            .Where(row => string.Equals(row.Comment, CommonConstants.HomeVisit, StringComparison.InvariantCultureIgnoreCase));
        homeVisitRows.ForEach(row =>
        {
            _occupationsAdder.AddToSpecificRoom(rooms, string.Empty, row);
            orderedRows.Remove(row);
        });
    }
}