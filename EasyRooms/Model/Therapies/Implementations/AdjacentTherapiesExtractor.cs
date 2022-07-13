namespace EasyRooms.Model.Therapies.Implementations;

public class AdjacentTherapiesExtractor : IAdjacentTherapiesExtractor
{
    public IReadOnlyCollection<Row> GetAdjacentRowsWithSamePatient(IEnumerable<Row> orderedRows, Row currentRow)
        => orderedRows
            .Where(row => row.Patient == currentRow.Patient)
            .OrderBy(row => row.StartTime)
            .Aggregate(new List<Row> { currentRow }, (acc, curr) =>
                curr.StartTimeAsTimeSpan == acc.Last().StartTimeAsTimeSpan + acc.Last().DurationAsTimeSpan
                    ? acc.Concat(new[] { curr }).ToList()
                    : acc);
}