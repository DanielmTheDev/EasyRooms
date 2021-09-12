using System.Collections.Generic;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Interfaces;

public interface IRowsCreator
{
    IEnumerable<Row> CreateRows(IEnumerable<string> words);
}
