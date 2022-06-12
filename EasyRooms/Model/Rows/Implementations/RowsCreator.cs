using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Constants;
using EasyRooms.Model.Rows.Interfaces;

namespace EasyRooms.Model.Rows.Implementations;

public class RowsCreator : IRowsCreator
{
    public IEnumerable<Row> CreateRows(IEnumerable<string> words)
    {
        var enumeratedWords = words.ToList();
        var rows = new List<Row>();
        while (enumeratedWords.Any())
        {
            GuardDuration(enumeratedWords[1]);
            var comment = GetAndRemoveRowComment(enumeratedWords);

            var newRow = new Row(
                enumeratedWords[0].Trim('(', ')'),
                enumeratedWords[1],
                enumeratedWords[2],
                enumeratedWords[3],
                enumeratedWords[4],
                enumeratedWords[5],
                comment);
            rows.Add(newRow);
            enumeratedWords.RemoveRange(0, CommonConstants.ElementsPerRow);
        }

        return rows;
    }

    private static string GetAndRemoveRowComment(List<string> enumeratedWords)
    {
        string comment;
        if (NextRowContainsComment(enumeratedWords))
        {
            comment = enumeratedWords[8];
            enumeratedWords.RemoveRange(6, 4);
        }
        else
        {
            comment = string.Empty;
        }

        return comment;
    }

    private static bool NextRowContainsComment(IReadOnlyList<string> enumeratedWords)
        => enumeratedWords.Count == 10
           || (enumeratedWords.Count > 10
               && enumeratedWords[0].TryParseTimeTrimmed(out var currentRowsTime)
               && enumeratedWords[6].TryParseTimeTrimmed(out var nextRowsTime)
               && int.TryParse(enumeratedWords[1], out var currentRowsDuration)
               && nextRowsTime < currentRowsTime.AddMinutes(currentRowsDuration)
               && enumeratedWords[10].TryParseTimeTrimmed(out _));

    private static void GuardDuration(string duration)
    {
        if (!int.TryParse(duration, out _))
        {
            throw new ArgumentException("Duration is not a number");
        }
    }
}