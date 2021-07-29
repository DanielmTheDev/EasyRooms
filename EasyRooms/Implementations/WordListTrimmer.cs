using EasyRooms.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Implementations
{
    public class WordListTrimmer : IWordListTrimmer
    {
        //TODO instead of skipWhile Personal I should check if it is time string by regex or something
        //TODO using modulo, find all therapists names. then cut at end of the list when name is used last
        public IEnumerable<string> TrimList(IEnumerable<string> words)
        {
            var trimmedList = words
                .SkipWhile(entry => !string.Equals(entry, Constants.Constants.PersonalString, StringComparison.OrdinalIgnoreCase))
                .Skip(1)
                .ToList();
            trimmedList.RemoveRange(trimmedList.Count() - 4, 4);
            return trimmedList;
        }
    }
}
