using EasyRooms.Model.Constants;
using EasyRooms.Model.Rows.Interfaces;

namespace EasyRooms.Model.Rows.Implementations;

public class RowsCreator : IRowsCreator
{
    public IEnumerable<Row> CreateRows(IEnumerable<string> words)
    {
        var enumeratedWords = words.ToList();
        var rows = new List<Row>();
        for (var i = 0; i < enumeratedWords.Count - 5; i += CommonConstants.ElementsPerRow)
        {
            GuardDuration(enumeratedWords[i + 1]);
            string comment;
            if (RowContainsComment(enumeratedWords, i))
            {
                comment = enumeratedWords[i + 2];
                enumeratedWords.RemoveAt(i + 2);
            }
            else
            {
                comment = string.Empty;
            }
            var newRow = new Row(
                enumeratedWords[i].Trim('(', ')'),
                enumeratedWords[i + 1],
                enumeratedWords[i + 2],
                enumeratedWords[i + 3],
                enumeratedWords[i + 4],
                enumeratedWords[i + 5],
                comment);
            rows.Add(newRow);
        }
        return rows;
    }

    private static bool RowContainsComment(IReadOnlyList<string> enumeratedWords, int i)
        => TimeOnly.TryParse(enumeratedWords[i].Trim('(', ')'), out _)
           && enumeratedWords.Count >= i + 7
           && TimeOnly.TryParse(enumeratedWords[i + 7], out _);

    private static void GuardDuration(string duration)
    {
        if (!int.TryParse(duration, out _))
        {
            throw new ArgumentException("Duration is not a number");
        }
    }
}