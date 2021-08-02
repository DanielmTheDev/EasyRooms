using EasyRooms.Models;
using System.Collections.Generic;

namespace EasyRooms.Interfaces
{
    public interface IRowsCreator
    {
        IEnumerable<Row> CreateRows(IEnumerable<string> words);
    }
}
