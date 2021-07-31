using EasyRooms.Extensions;
using EasyRooms.Interfaces;
using System.Collections.Generic;

namespace EasyRooms.Implementations
{
    public class DayPlanParser : IDayPlanParser
    {
        private readonly IXpsWordsExtractor _xpsWordsExtractor;
        private readonly IRowsCreator _rowsCreator;

        public DayPlanParser(IXpsWordsExtractor xpsWordsExtractor, IRowsCreator rowsCreator)
        {
            _xpsWordsExtractor = xpsWordsExtractor;
            _rowsCreator = rowsCreator;
        }


        public IEnumerable<Row> ParseDayPlan(string path)
        {
            var words = _xpsWordsExtractor
                .ExtractWords(path)
                .RemoveHomeVisitRows()
                .RemovePageEntries()
                .RemovePauseRows()
                .RemoveCommentaries()
                .RemoveHeaders()
                .RemoveLegend()
                .RemoveEnd();


            return _rowsCreator.CreateRows(words);
        }
    }
}
