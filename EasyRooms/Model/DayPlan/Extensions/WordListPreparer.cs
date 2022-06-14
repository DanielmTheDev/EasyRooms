using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Constants;

namespace EasyRooms.Model.DayPlan.Extensions;

public static class WordListPreparer
{
    public static IEnumerable<string> RemoveIgnorableComments(this IEnumerable<string> words)
    {
        var enumeratedWords = words.ToList();
        var indicesOfIgnorableComments = Enumerable.Range(2, enumeratedWords.Count - 5)
            .Where(i => enumeratedWords[i - 2].TryParseTimeTrimmed(out _)
                        && int.TryParse(enumeratedWords[i - 1], out _)
                        && enumeratedWords[i + 5].TryParseTimeTrimmed(out _));
        indicesOfIgnorableComments
            .OrderByDescending(i => i)
            .ForEach(i => enumeratedWords.RemoveAt(i));
        return enumeratedWords;
    }

    public static IEnumerable<string> RemovePauseRows(this IEnumerable<string> words)
    {
        var enumeratedWords = words.ToList();
        var indicesOfPauseEntries = Enumerable.Range(0, enumeratedWords.Count)
            .Where(i => enumeratedWords[i].Contains(CommonConstants.Pause))
            .ToList();

        indicesOfPauseEntries
            .OrderByDescending(i => i)
            .ToList()
            .ForEach(index =>
            {
                var indexOfPreviousDateTime = IndexOfPreviousDateTime(enumeratedWords, index);
                var indexOfNextDateTime = IndexOfNextDateTime(enumeratedWords, index);
                enumeratedWords.RemoveRange(indexOfPreviousDateTime, indexOfNextDateTime + 1 - indexOfPreviousDateTime);
            });
        return enumeratedWords;
    }

    public static IEnumerable<string> RemovePageEntries(this IEnumerable<string> words)
    {
        var enumeratedWords = words.ToList();
        var indicesOfPageEntries = Enumerable.Range(0, enumeratedWords.Count)
            .Where(i => enumeratedWords[i].Contains(CommonConstants.Page))
            .ToList();

        indicesOfPageEntries
            .OrderByDescending(i => i)
            .ToList()
            .ForEach(index => enumeratedWords.RemoveRange(index, 2));
        return enumeratedWords;
    }

    public static IEnumerable<string> RemoveHeaders(this IEnumerable<string> words)
    {
        var enumeratedWords = words.ToList();
        var indicesOfHeaders = Enumerable.Range(0, enumeratedWords.Count)
            .Where(i => string.Equals(enumeratedWords[i], CommonConstants.DayPlan, StringComparison.OrdinalIgnoreCase))
            .ToList();

        indicesOfHeaders
            .OrderByDescending(i => i)
            .ToList()
            .ForEach(index => enumeratedWords.RemoveRange(index, 13));
        return enumeratedWords;
    }

    public static IEnumerable<string> RemoveEndOfListEntry(this IEnumerable<string> words)
    {
        var enumeratedWords = words.ToList();
        enumeratedWords.RemoveAll(entry => string.Equals(entry, CommonConstants.EndOfList, StringComparison.OrdinalIgnoreCase));
        return enumeratedWords;
    }

    public static IEnumerable<string> RemoveLegendEntries(this IEnumerable<string> words)
    {
        var enumeratedWords = words.ToList();
        enumeratedWords.RemoveAll(entry => string.Equals(entry, CommonConstants.Legend, StringComparison.OrdinalIgnoreCase));
        return enumeratedWords;
    }

    private static int IndexOfNextDateTime(IReadOnlyList<string> enumeratedWords, int index)
    {
        for (var i = index; i < enumeratedWords.Count; i++)
        {
            if (DateTime.TryParse(enumeratedWords[i], out _))
            {
                return i - 1;
            }
        }

        throw new ArgumentException("No DateTime found after pause");
    }

    private static int IndexOfPreviousDateTime(IReadOnlyList<string> enumeratedWords, int index)
    {
        for (var i = index; i >= 0; i--)
        {
            if (DateTime.TryParse(enumeratedWords[i], out _))
            {
                return i;
            }
        }

        throw new ArgumentException("No DateTime found before pause");
    }
}