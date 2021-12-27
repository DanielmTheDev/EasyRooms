using System.Collections.Generic;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Rows.Interfaces;

public interface IRowsCreator
{
    IEnumerable<Row> CreateRows(IEnumerable<string> words);
}