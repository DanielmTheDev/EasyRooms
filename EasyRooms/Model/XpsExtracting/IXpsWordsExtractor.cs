using System.Collections.Generic;

namespace EasyRooms.Model.XpsExtracting;

public interface IXpsWordsExtractor
{
    IEnumerable<string> ExtractWords(string filePath);
}
