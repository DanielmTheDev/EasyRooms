namespace EasyRooms.Model.Comparison.Implementations;

public static class OverlapChecker
{
    public static bool OccupationsOverlap(TimeSpan startTime, TimeSpan endTime, Occupation occupation2, int savedOptionsBuffer)
        => startTime < occupation2.EndTime.Add(TimeSpan.FromMinutes(savedOptionsBuffer))
           && occupation2.StartTime < endTime.Add(TimeSpan.FromMinutes(savedOptionsBuffer));
}