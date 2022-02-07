namespace EasyRooms.Model.Rows.Interfaces;

public interface IRowsCreator
{
    IEnumerable<Row> CreateRows(IEnumerable<string> words);
}