using System.Collections.Generic;

namespace EasyRooms.Interfaces
{
    public interface IHomeVisitRowsRemover
    {
        IEnumerable<string> RemoveHomeVisitRows(IEnumerable<string> words);
    }
}
