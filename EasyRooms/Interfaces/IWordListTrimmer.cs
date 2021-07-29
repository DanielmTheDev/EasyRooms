using System.Collections.Generic;

namespace EasyRooms.Interfaces
{
    public interface IWordListTrimmer
    {
        IEnumerable<string> TrimList(string[] words);
    }
}
