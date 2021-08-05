using EasyRooms.Interfaces;
using EasyRooms.Models;
using System;
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
                //GuardDuration(enumeratedWords[i + 1]);
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

        private void GuardDuration(string duration)
        {
            if(!int.TryParse(duration, out var _))
            {
                throw new ArgumentException("Duration is not a number");
            }
        }
    }
}
