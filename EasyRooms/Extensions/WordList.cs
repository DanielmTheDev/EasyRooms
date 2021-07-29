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

        //todo pause needs to delete between daystime and name of therapist
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
                .ForEach(index => enumeratedWords.RemoveRange(index - 4, CommonConstants.ElementsPerRowWithoutHouseVisitEntry));
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

        public static IEnumerable<string> RemoveEnd(this IEnumerable<string> words)
        {
            var enumeratedWords = words.ToList();
            enumeratedWords.RemoveAll(entry => string.Equals(entry, CommonConstants.EndOfList, StringComparison.OrdinalIgnoreCase));
            return enumeratedWords;
        }

        public static IEnumerable<string> RemoveLegend(this IEnumerable<string> words)
        {
            var enumeratedWords = words.ToList();
            enumeratedWords.RemoveAll(entry => string.Equals(entry, CommonConstants.Legend, StringComparison.OrdinalIgnoreCase));
            return enumeratedWords;
        }
    }
}
