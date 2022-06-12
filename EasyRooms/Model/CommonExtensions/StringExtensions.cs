namespace EasyRooms.Model.CommonExtensions;

public static class StringExtensions
{
    public static bool TryParseTimeTrimmed(this string theString, out TimeOnly time)
        => TimeOnly.TryParse(theString.Trim('(', ')'), out time);
}