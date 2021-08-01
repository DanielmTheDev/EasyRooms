using EasyRooms.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Extensions
{
    public static class WordList
    {
        public static IEnumerable<string> RemoveHomeVisitRows(this IEnumerable<string> words)
        {
            var strings = words.ToList();
            var indicesOfHomeVisitEntries = Enumerable.Range(0, strings.Count)
                .Where(i => string.Equals(strings[i], CommonConstants.HomeVisit, StringComparison.OrdinalIgnoreCase))
                .ToList();

            indicesOfHomeVisitEntries
                .OrderByDescending(i => i)
                .ToList()
                .ForEach(index => strings.RemoveRange(index - 2, CommonConstants.ElementsPerRowWithHouseVisitEntry));
            return strings;
        }

        public static IEnumerable<string> RemovePauseRows(this IEnumerable<string> words)
        {
            var enumeratedWords = words.ToList();
            var indicesOfPauseEntries = Enumerable.Range(0, enumeratedWords.Count)
                .Where(i => enumeratedWords[i].ToUpperInvariant().Contains(CommonConstants.Pause)
                    && i % CommonConstants.ElementsPerRowWithoutHouseVisitEntry == 3)
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

        public static IEnumerable<string> RemoveCommentaries(this IEnumerable<string> words)
        {
            var enumeratedWords = words.ToList();
            var commentaryIndices = words
                .Select((word, i) => (word, index: i))
                .Where(wordWithIndex => TimeSpan.TryParse(wordWithIndex.word, out var _)
                    && TimeSpan.TryParse(enumeratedWords[wordWithIndex.index + 3], out var _))
                .ToList();

            commentaryIndices
             .ForEach(commentary => enumeratedWords.RemoveRange(commentary.index, 4));
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

        private static int IndexOfNextDateTime(List<string> enumeratedWords, int index)
        {
            for (int i = index; i < enumeratedWords.Count; i++)
            {
                if (DateTime.TryParse(enumeratedWords[i], out var _))
                {
                    return i - 1;
                }
            }
            throw new ArgumentException("No DateTime found after pause");
        }

        private static int IndexOfPreviousDateTime(List<string> enumeratedWords, int index)
        {
            for (int i = index; i >= 0; i--)
            {
                if (DateTime.TryParse(enumeratedWords[i], out var _))
                {
                    return i;
                }
            }
            throw new ArgumentException("No DateTime found before pause");
        }
    }
}
