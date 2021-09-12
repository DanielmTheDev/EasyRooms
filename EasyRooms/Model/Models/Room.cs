using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Model.Models;

public record Room
{
    public string Name { get; set; }
    public int Priority { get; set; }
    //todo encapsulate this list
    public IList<Occupation> Occupations { get; }

    public Room(string name, int priority)
    {
        Name = name;
        Priority = priority;
        Occupations = new List<Occupation>();
    }

    public Room AddOccupation(Occupation occupation)
    {
        Occupations.Add(occupation);
        return this;
    }

    public bool IsOccupiedAt(TimeSpan startTime, TimeSpan endTime, int bufferInMinutes)
        => Occupations.Any(occupation =>
        startTime < GetEndTimeWithBuffer(occupation.EndTime, bufferInMinutes)
        && endTime > occupation.StartTime);

    private static TimeSpan GetEndTimeWithBuffer(TimeSpan endTime, int bufferInMinutes)
    {
        var minutes = TimeSpan.FromMinutes(bufferInMinutes);
        return endTime.Add(minutes);
    }
}
