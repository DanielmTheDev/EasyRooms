using EasyRooms.Model.CommonExtensions;

namespace EasyRooms.Model.Therapies.Implementations;

// adds a row to a specific room if its comment field matches the room name
public class CommentTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public CommentTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var rowsWithRoomComment = orderedRows
            .Where(row => !string.IsNullOrEmpty(row.Comment)
                          && rooms.Any(room => string.Equals(room.Name, row.Comment, StringComparison.InvariantCultureIgnoreCase)));
        rowsWithRoomComment.ForEach(row =>
        {
            _occupationsAdder.AddToSpecificRoom(rooms, row.Comment, row);
            orderedRows.Remove(row);
        });
    }
}