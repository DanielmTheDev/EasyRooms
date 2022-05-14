

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
#nullable disable

namespace EasyRooms.Model.Rooms.Models;

public class Occupation
{
    public string Therapist { get; set; }
    public string Patient { get; set; }
    public string Comment { get; set; }
    public string TherapyShort { get; set; }
    public string TherapyLong { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    // ReSharper disable once UnusedMember.Global
    public Occupation()
    {
    }

    public Occupation(Row row, TimeSpan startTime, TimeSpan endTime) : this(row.Therapist, row.Patient, row.Comment, row.TherapyShort, row.TherapyLong, startTime, endTime)
    {
    }

    public Occupation(string therapist, string patient, string comment, string therapyShort, string therapyLong, TimeSpan startTime, TimeSpan endTime)
    {
        Therapist = therapist;
        Patient = patient;
        Comment = comment;
        TherapyShort = therapyShort;
        TherapyLong = therapyLong;
        StartTime = startTime;
        EndTime = endTime;
    }
}