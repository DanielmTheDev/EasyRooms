namespace EasyRooms.Model.Therapies.Interfaces;

public interface IAdjacentTherapiesExtractor
{
    IReadOnlyCollection<Row> GetAdjacentRowsWithSamePatient(IEnumerable<Row> orderedRows, Row currentRow);
}