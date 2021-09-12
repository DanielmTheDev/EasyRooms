using System.Collections.Generic;

namespace EasyRooms.Model.Interfaces;

public interface IXpsWordsExtractor
{
    IEnumerable<string> ExtractWords(string filePath);
}
