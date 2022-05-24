using EasyRooms.Model.DayPlan.Extensions;
using EasyRooms.Model.DayPlan.Interfaces;
using EasyRooms.Model.DayPlan.Models;
using EasyRooms.Model.Rows.Interfaces;
using EasyRooms.Model.XpsExtracting.Interfaces;

namespace EasyRooms.Model.DayPlan.Implementations;

public class DayPlanParser : IDayPlanParser
{
    private readonly IXpsWordsExtractor _xpsWordsExtractor;
    private readonly IRowsCreator _rowsCreator;

    public DayPlanParser(IXpsWordsExtractor xpsWordsExtractor, IRowsCreator rowsCreator)
        => (_xpsWordsExtractor, _rowsCreator) = (xpsWordsExtractor, rowsCreator);

    public ParsedPlan ParseRows(string path)
        => CreateParsedPlan(ExtractWords(path));

    private ParsedPlan CreateParsedPlan(IReadOnlyCollection<string> extractedWords)
    {
        var date = DateOnly.Parse(extractedWords.First(word => DateOnly.TryParse(word, out _)));
        var cleanedWords = extractedWords
            .RemoveHomeVisitRows()
            .RemovePageEntries()
            .RemovePauseRows()
            .RemoveAlmostEmptyRows()
            .RemoveCommentaries()
            .RemoveHeaders()
            .RemoveLegendEntries()
            .RemoveEndOfListEntry();

        return new(date, _rowsCreator.CreateRows(cleanedWords));
    }

    private List<string> ExtractWords(string path)
        => _xpsWordsExtractor
            .ExtractWords(path)
            .ToList();
}