using EasyRooms.Model.DayPlan.Extensions;
using EasyRooms.Model.DayPlan.Interfaces;
using EasyRooms.Model.Rows.Interfaces;
using EasyRooms.Model.XpsExtracting.Interfaces;

namespace EasyRooms.Model.DayPlan.Implementations;

public class DayPlanParser : IDayPlanParser
{
    private readonly IXpsWordsExtractor _xpsWordsExtractor;
    private readonly IRowsCreator _rowsCreator;

    public DayPlanParser(IXpsWordsExtractor xpsWordsExtractor, IRowsCreator rowsCreator)
        => (_xpsWordsExtractor, _rowsCreator) = (xpsWordsExtractor, rowsCreator);

    public IEnumerable<Row> ParseDayPlan(string path)
    {
        var words = _xpsWordsExtractor
            .ExtractWords(path)
            .RemoveHomeVisitRows()
            .RemovePageEntries()
            .RemovePauseRows()
            .RemoveCommentaries()
            .RemoveHeaders()
            .RemoveLegendEntries()
            .RemoveEndOfListEntry();

        return _rowsCreator.CreateRows(words);
    }
}