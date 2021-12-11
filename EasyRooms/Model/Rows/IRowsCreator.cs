using System.Collections.Generic;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Rows;

public interface IRowsCreator
{
    IEnumerable<Row> CreateRows(IEnumerable<string> words);
}