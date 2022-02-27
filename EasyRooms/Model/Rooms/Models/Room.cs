namespace EasyRooms.Model.Rooms.Models;

public record Room(string Name)
{
    public bool IsPartnerRoom { get; set; }
    public IList<Occupation> Occupations { get; set; } = new List<Occupation>();

    public void AddOccupation(Occupation occupation)
        => Occupations.Add(occupation);

    public bool IsOccupiedAt(TimeSpan startTime, TimeSpan endTime, int bufferInMinutes)
        => Occupations.Any(occupation =>
            startTime < GetEndTimeWithBuffer(occupation.EndTime, bufferInMinutes)
            && endTime > occupation.StartTime);

    private static TimeSpan GetEndTimeWithBuffer(TimeSpan endTime, int bufferInMinutes)
    {
        var minutes = TimeSpan.FromMinutes(bufferInMinutes);
        return endTime.Add(minutes);
    }

    public void OrderOccupations()
    {
        Occupations = Occupations.OrderBy(occupation => occupation.StartTime).ToList();
    }
}