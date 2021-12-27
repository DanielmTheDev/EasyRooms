using System.Collections.Generic;

namespace EasyRooms.Model.XpsExtracting.Interfaces;

public interface IXpsWordsExtractor
{
    IEnumerable<string> ExtractWords(string filePath);
}