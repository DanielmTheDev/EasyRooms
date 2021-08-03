using System;

namespace EasyRooms.Models
{
    public record Occupation(string Therapist, string Patient, string TherapyShort, string TherapyLong, TimeSpan From, TimeSpan To)
    {
        public Occupation(Row row) : this(row.Therapist, row.Patient, row.TherapyShort,
            row.TherapyLong, TimeSpan.Parse(row.StartTime), TimeSpan.Parse(row.StartTime).Add(TimeSpan.Parse(row.Duration)))
        { }
    }
}