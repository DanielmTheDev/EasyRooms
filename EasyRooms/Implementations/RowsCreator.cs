using EasyRooms.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Implementations
{
    public class RowsCreator : IRowsCreator
    {
        //TODO somehow refactor
        public IEnumerable<Row> CreateRows(IEnumerable<string> words)
        {
            var enumeratedWords = words.ToList();
            var rows = new List<Row>();
            for (int i = 0; i < words.Count() - 5; i += Constants.CommonConstants.ElementsPerRow)
            {
                var newRow = new Row(
                enumeratedWords[i],
                enumeratedWords[i + 1],
                enumeratedWords[i + 2],
                enumeratedWords[i + 3],
                enumeratedWords[i + 4],
                enumeratedWords[i + 5]);
                rows.Add(newRow);
            }
            return rows;
        }
    }
}
