namespace EasyRooms.Model.CommonExtensions;

public static class StringExtensions
{
    public static bool TryParseTimeTrimmed(this string theString, out TimeOnly time)
        => TimeOnly.TryParse(theString.Trim('(', ')'), out time);

    public static bool EqualsInvariant(this string theString, string other)
        => string.Equals(theString, other, StringComparison.InvariantCultureIgnoreCase);

    public static bool ContainsInvariant(this string theString, string other)
        => theString.Contains(other, StringComparison.InvariantCultureIgnoreCase);

    public static bool TryParseDuration(this string theString, out int result)
    {
        var allFactorsParsable = theString
            .Split('x')
            .All(s => int.TryParse(s, out _));
        if (!allFactorsParsable)
        {
            result = -1;
            return false;
        }

        result = theString.ParseDuration();
        return true;
    }

    public static int ParseDuration(this string theString)
        => theString
            .Split('x')
            .Select(int.Parse)
            .Aggregate((total, next) => total * next);
}