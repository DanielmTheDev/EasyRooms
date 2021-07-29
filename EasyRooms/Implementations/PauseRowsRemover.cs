using EasyRooms.Interfaces;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Implementations
{
    public class PauseRowsRemover : IPauseRowsRemover
    {
        public IEnumerable<string> RemovePauseRows(IEnumerable<string> words)
        {
            var enumeratedWords = words.ToList();
            var indicesOfPauseEntries = Enumerable.Range(0, enumeratedWords.Count)
                .Where(i => enumeratedWords[i].Contains(Constants.Constants.Pause) && i % 6 == 2)
                .ToList();

            indicesOfPauseEntries
                .OrderByDescending(i => i)
                .ToList()
                .ForEach(index => enumeratedWords.RemoveRange(index - 3, Constants.Constants.ElementsPerRowWithoutHouseVisitEntry));
            return enumeratedWords;
        }
    }
}
