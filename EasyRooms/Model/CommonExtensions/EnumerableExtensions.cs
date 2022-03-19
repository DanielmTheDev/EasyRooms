namespace EasyRooms.Model.CommonExtensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> callback)
    {
        var enumeratedList = source.ToList();
        enumeratedList.ForEach(callback);
        return enumeratedList;
    }
}