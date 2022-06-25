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
            Row newRow;
            GuardDuration(enumeratedWords[1]);
            if (RowIsBlock(enumeratedWords))
            {
                newRow = ExtractBlockRow(enumeratedWords);
                enumeratedWords.RemoveRange(0, 4);
            }
            else
            {
                var comment = GetAndRemoveComment(enumeratedWords);
                newRow = ExtractNormalRow(enumeratedWords, comment);
                enumeratedWords.RemoveRange(0, CommonConstants.ElementsPerRow);
            }

            rows.Add(newRow);
        }

        return rows;
    }

    private static Row ExtractNormalRow(IReadOnlyList<string> enumeratedWords, string comment)
        => new(
            enumeratedWords[0].Trim('(', ')'),
            enumeratedWords[1].ParseDuration(),
            enumeratedWords[2],
            enumeratedWords[3],
            enumeratedWords[4],
            enumeratedWords[5],
            comment);

    private static Row ExtractBlockRow(IReadOnlyList<string> enumeratedWords)
        => new(
            enumeratedWords[0],
            enumeratedWords[1].ParseDuration(),
            string.Empty,
            string.Empty,
            string.Empty,
            enumeratedWords[3],
            enumeratedWords[2]);

    private static bool RowIsBlock(IReadOnlyList<string> enumeratedWords)
        => enumeratedWords.Count == 4 || enumeratedWords[4].TryParseTimeTrimmed(out _);

    private static bool NextRowIsComment(IReadOnlyList<string> enumeratedWords)
        => enumeratedWords.Count == 10 && enumeratedWords[5] == enumeratedWords[9]
           || enumeratedWords.Count > 10
           && enumeratedWords[0].TryParseTimeTrimmed(out var currentRowsTime)
           && enumeratedWords[6].TryParseTimeTrimmed(out var nextRowsTime)
           && enumeratedWords[1].TryParseDuration(out var currentRowsDuration)
           && enumeratedWords[5] == enumeratedWords[9]
           && enumeratedWords[10].TryParseTimeTrimmed(out _)
           && nextRowsTime < currentRowsTime.AddMinutes(currentRowsDuration);

    private static bool IsHomeVisit(IReadOnlyList<string> enumeratedWords)
        => enumeratedWords[2].EqualsInvariant(CommonConstants.HomeVisit);

    private static string GetAndRemoveComment(List<string> enumeratedWords)
    {
        string comment;
        if (IsHomeVisit(enumeratedWords))
        {
            comment = enumeratedWords[2];
            enumeratedWords.RemoveAt(2);
        }
        else if (NextRowIsComment(enumeratedWords))
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

    private static void GuardDuration(string duration)
    {
        if (!duration.TryParseDuration(out _))
        {
            throw new ArgumentException($"Duration {duration} is not a number");
        }
    }
}