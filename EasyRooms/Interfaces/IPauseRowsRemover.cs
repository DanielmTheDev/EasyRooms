using System.Collections.Generic;

namespace EasyRooms.Interfaces
{
    public interface IPauseRowsRemover
    {
        IEnumerable<string> RemovePauseRows(IEnumerable<string> words);
    }
}
