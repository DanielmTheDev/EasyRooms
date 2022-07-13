using EasyRooms.Model.CommonExtensions;

namespace EasyRooms.Model.Therapies.Implementations;

// adds a row to a specific room if its comment field matches the room name
public class CommentTherapiesAdder : ITherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;
    private readonly IAdjacentTherapiesExtractor _adjacentTherapiesExtractor;

    public CommentTherapiesAdder(IOccupationsAdder occupationsAdder, IAdjacentTherapiesExtractor adjacentTherapiesExtractor)
    {
        _occupationsAdder = occupationsAdder;
        _adjacentTherapiesExtractor = adjacentTherapiesExtractor;
    }

    public void Add(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var rowsForSpecificRooms = GetRowsForSpecificRooms(rooms, orderedRows);
        AddMissingCommentToAdjacentRows(rowsForSpecificRooms);
        rowsForSpecificRooms.ForEach(row =>
        {
            _occupationsAdder.AddToSpecificRoom(rooms, row.Comment, row);
            orderedRows.Remove(row);
        });
    }

    private List<Row> GetRowsForSpecificRooms(IList<Room> rooms, IReadOnlyCollection<Row> orderedRows)
        => orderedRows.Aggregate(new List<Row>(), (accRows, currentRow) =>
            !string.IsNullOrEmpty(currentRow.Comment) && rooms.Any(room => room.Name.EqualsInvariant(currentRow.Comment))
                ? accRows
                    .Concat(_adjacentTherapiesExtractor.GetAdjacentRowsWithSamePatient(orderedRows, currentRow))
                    .ToList()
                : accRows);

    private static void AddMissingCommentToAdjacentRows(IEnumerable<Row> rowsForSpecificRooms)
        => rowsForSpecificRooms
            .GroupBy(row => row.Patient, row => row)
            .ForEach(grouping =>
            {
                var comment = grouping.First(row => !string.IsNullOrEmpty(row.Comment)).Comment;
                grouping.ToList().ForEach(row => row.Comment = comment);
            });
}