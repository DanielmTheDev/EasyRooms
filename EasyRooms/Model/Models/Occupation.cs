using System;

namespace EasyRooms.Model.Models;

public record Occupation(string Therapist, string Patient, string TherapyShort, string TherapyLong, TimeSpan StartTime, TimeSpan EndTime)
{
    public Occupation(Row row, TimeSpan startTime, TimeSpan endTime) : this(row.Therapist,row.Patient, row.TherapyShort, row.TherapyLong, startTime, endTime)
    {
    }
}
